using System.Drawing.Imaging;
using System.Resources;
using TinyDesktopCapture.Properties;

namespace TinyDesktopCapture {
    public partial class MainForm : Form {
        #region フィールド

        /// <summary>
        /// マウスドラッグ情報
        /// </summary>
        private MouseDragInfo _mouseInfo = new MouseDragInfo();

        /// <summary>
        /// 設定
        /// </summary>
        private Config _config = new Config();

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm() {
            InitializeComponent();

            {   // メインフォーム
                this.Icon = Resources.Picture;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.KeyPreview = true;

                this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
                this.MouseDown += new MouseEventHandler(MouseDownEventHandler);
                this.MouseUp += new MouseEventHandler(MainForm_MouseUp);
                this.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            }

            {   // コンテキストメニュー
                this.設定ToolStripMenuItem.Click += new EventHandler(設定ToolStripMenuItem_Click);
                this.CopyToClipboardToolStripMenuItem.Click += new EventHandler(CopyToClipboardToolStripMenuItem_Click);
            }

            {   // 設定の取得
                _config.ImageType = Settings.Default.ImageType;
                _config.Magnification = Settings.Default.ImageMagnification;

            }

            this.SetStyle(
                ControlStyles.DoubleBuffer |         // 描画をバッファで実行する
                ControlStyles.UserPaint |            // 描画は（OSでなく）独自に行う
                ControlStyles.AllPaintingInWmPaint,  // WM_ERASEBKGND を無視する
                true                                 // 指定したスタイルを適用する
                );

            // get desktop size
            var currentScreen = Screen.FromControl(this);
            var location = new Point(currentScreen.Bounds.X, currentScreen.Bounds.Y);
            var desktop = new Rectangle(location, currentScreen.Bounds.Size);

            // 画面全体をキャプチャする
            this.BackgroundImage = this.GetCaptureImage(desktop);
        }

        #endregion コンストラクタ

        #region フォームのイベントハンドラ

        #region MainForm_KeyDown

        /// <summary>
        /// キーが押された時に呼ばれます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion MainForm_KeyDown

        #region MainForm_MouseUp

        /// <summary>
        /// マウスクリック終了時に呼ばれます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_MouseUp(object sender, MouseEventArgs e) {
            // 選択範囲ドラッグ終了
            Cursor.Current = Cursors.Default;

            _mouseInfo.EndDrag();
        }

        #endregion MainForm_MouseUp

        #region MainForm_MouseMove

        /// <summary>
        /// マウスカーソル移動字に呼ばれます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_MouseMove(object sender, MouseEventArgs e) {

            // ドラッグ開始前は十字にする
            if (_mouseInfo.Status == MouseDragInfo.DragStaus.Off)
            {
                this.Refresh();
                DrawCross(e.Location);
            } else if (_mouseInfo.Status == MouseDragInfo.DragStaus.On)
            {
                // 選択範囲を描画する
                _mouseInfo.CalcDragRectangle(e.Location);

                this.Refresh();
                DrawSelectedArea(_mouseInfo.DragRectangle);
            }
        }

        /// <summary>
        /// 指定された位置を中心にした十字を描画します。
        /// </summary>
        /// <param name="centerPoint">中心の位置</param>
        private void DrawCross(Point centerPoint) {
            using (Graphics g = this.CreateGraphics())
            {
                if (Screen.PrimaryScreen is null)
                    throw new Exception("Screen.PrimaryScreen is null");

                Rectangle rect = Screen.PrimaryScreen.WorkingArea;

                // 水平線
                g.DrawLine(
                    Pens.Red,
                    rect.Left,
                    centerPoint.Y,
                    rect.Right,
                    centerPoint.Y);

                // 垂直線
                g.DrawLine(
                    Pens.Red,
                    centerPoint.X,
                    rect.Top,
                    centerPoint.X,
                    rect.Bottom);
            }
        }

        /// <summary>
        /// 選択範囲を描画します。
        /// </summary>
        /// <param name="rect"></param>
        private void DrawSelectedArea(Rectangle rect) {
            using (Graphics g = this.CreateGraphics())
            {
                g.DrawRectangle(Pens.Red, rect);
            }
        }

        #endregion MainForm_MouseMove

        #endregion フォームのイベントハンドラ

        #region MouseDownEventHandler

        /// <summary>
        /// マウスがクリックされた時に呼ばれます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseDownEventHandler(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(e.Location);
                return;
            }

            if (_mouseInfo.Status == MouseDragInfo.DragStaus.Off)
            {
                // ドラッグ開始
                _mouseInfo.BeginDrag(e.Location);

            } else if (_mouseInfo.Status == MouseDragInfo.DragStaus.Complete)
            {
                // TODO: クリップボードに取り込む
                //SetImageToClip();
                //this.Close();
            }
        }

        #region SetImageToClip

        /// <summary>
        /// 選択範囲のイメージををクリップボードにコピーします。
        /// </summary>
        private void SetImageToClip() {
            // 赤枠を消す
            this.Refresh();

            // 選択範囲をクリップボードにコピー
            Image img = GetCaptureImage(_mouseInfo.DragRectangle);

            // 倍率を変更
            Image newImg = MagnifyImage(img, _config.Magnification);
            img.Dispose();

            // 画像形式を変換
            Image newImg2 = ConvertImage(newImg);
            newImg.Dispose();

            Clipboard.SetImage(newImg2);
        }

        #endregion SetImageToClip

        #endregion MouseDownEventHandler


        #region コンテキストメニュー

        /// <summary>
        /// 設定メニューがクリックされた時に呼ばれます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void 設定ToolStripMenuItem_Click(object sender, EventArgs e) {
            using (ConfigForm frm = new ConfigForm())
            {
                frm.ShowDialog(this);
                _config.ImageType = frm.ImageType;
                _config.Magnification = frm.Magnification;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyToClipboardToolStripMenuItem_Click(object sender, EventArgs e) {
            // クリップボードに取り込む
            SetImageToClip();
            this.Close();
        }

        #endregion コンテキストメニュー

        #region 画像処理

        #region ConvertImage

        /// <summary>
        /// 画像の形式を変換します。
        /// </summary>
        /// <param name="source">変換する画像</param>
        /// <returns>変換された画像</returns>
        private Image ConvertImage(Image source) {

            ImageFormat format;

            if ("png".Equals(_config.ImageType))
            {
                format = ImageFormat.Png;
            } else if ("bitmap".Equals(_config.ImageType))
            {
                format = ImageFormat.Bmp;
            } else if ("gif".Equals(_config.ImageType))
            {
                format = ImageFormat.Gif;
            } else if ("jpeg".Equals(_config.ImageType))
            {
                format = ImageFormat.Jpeg;
            } else
            {
                format = ImageFormat.Bmp;
            }

            string imgPath = Path.Combine(Application.StartupPath, "tempimg");
            source.Save(imgPath, format);
            return Image.FromFile(imgPath);
        }

        #endregion ConvertImage

        #region GetCaptureImage

        /// <summary>
        /// 指定範囲をキャプチャした画像を取得します。
        /// </summary>
        /// <param name="rect">キャプチャする範囲</param>
        /// <returns>画像</returns>
        private System.Drawing.Image GetCaptureImage(System.Drawing.Rectangle rect) {
            System.Drawing.Image img = new System.Drawing.Bitmap(
                                            rect.Width,
                                            rect.Height,
                                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img))
            {
                g.CopyFromScreen(
                    rect.X,
                    rect.Y,
                    0,
                    0,
                    rect.Size,
                    System.Drawing.CopyPixelOperation.SourceCopy);
            }

            return img;
        }

        #endregion GetCaptureImage

        /// <summary>
        /// 画像の倍率を変更します。
        /// </summary>
        /// <param name="img">画像</param>
        /// <param name="magnification">倍率</param>
        /// <returns>変更された画像</returns>
        private Image MagnifyImage(Image img, decimal magnification) {

            // TODO:リファクタする
            float fMagnification = (float)(magnification / 100);

            Bitmap newImg = new Bitmap((int)(img.Width * fMagnification), (int)(img.Height * fMagnification));

            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.ResetTransform();
                g.ScaleTransform(fMagnification, fMagnification);
                g.DrawImage(img, 0, 0);
            }

            return newImg;
        }

        #endregion 画像処理

        #region インナークラス

        /// <summary>
        /// 設定
        /// </summary>
        private class Config {

            /// <summary>
            /// 画像の形式
            /// </summary>
            internal string ImageType { get; set; }

            /// <summary>
            /// 画像の倍率
            /// </summary>
            internal decimal Magnification { get; set; }
        }

        #endregion インナークラス

    }
}