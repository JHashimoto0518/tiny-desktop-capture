using System.Drawing.Imaging;
using System.Resources;
using TinyDesktopCapture.Properties;

namespace TinyDesktopCapture {
    public partial class MainForm : Form {
        #region �t�B�[���h

        /// <summary>
        /// �}�E�X�h���b�O���
        /// </summary>
        private MouseDragInfo _mouseInfo = new MouseDragInfo();

        /// <summary>
        /// �ݒ�
        /// </summary>
        private Config _config = new Config();

        #endregion �t�B�[���h

        #region �R���X�g���N�^

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MainForm() {
            InitializeComponent();

            {   // ���C���t�H�[��
                this.Icon = Resources.Picture;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.KeyPreview = true;

                this.Shown += new EventHandler(MainForm_Shown);
                this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
                this.MouseDown += new MouseEventHandler(MouseDownEventHandler);
                this.MouseUp += new MouseEventHandler(MainForm_MouseUp);
                this.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            }

            {   // �R���e�L�X�g���j���[
                this.�ݒ�ToolStripMenuItem.Click += new EventHandler(�ݒ�ToolStripMenuItem_Click);
                this.CopyToClipboardToolStripMenuItem.Click += new EventHandler(CopyToClipboardToolStripMenuItem_Click);
            }

            {   // �ݒ�̎擾
                _config.ImageType = Settings.Default.ImageType;
                _config.Magnification = Settings.Default.ImageMagnification;

            }

            this.SetStyle(
                ControlStyles.DoubleBuffer |         // �`����o�b�t�@�Ŏ��s����
                ControlStyles.UserPaint |            // �`��́iOS�łȂ��j�Ǝ��ɍs��
                ControlStyles.AllPaintingInWmPaint,  // WM_ERASEBKGND �𖳎�����
                true                                 // �w�肵���X�^�C����K�p����
                );
        }

        #endregion �R���X�g���N�^

        #region �t�H�[���̃C�x���g�n���h��

        #region MainForm_Shown

        /// <summary>
        /// �t�H�[�������߂ĕ\�����ꂽ���ɌĂ΂�܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_Shown(object sender, EventArgs e) {
            // ��ʑS�̂��L���v�`������
            this.Hide();
            System.Threading.Thread.Sleep(200);
            this.BackgroundImage = this.GetCaptureImage(new Rectangle(this.Location, this.Size));
            this.Show();

            // TODO:�J�[�\�������ɖ߂��Ă��܂�
            Cursor.Current = Cursors.Cross;
        }

        #endregion MainForm_Shown

        #region MainForm_KeyDown

        /// <summary>
        /// �L�[�������ꂽ���ɌĂ΂�܂��B
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
        /// �}�E�X�N���b�N�I�����ɌĂ΂�܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_MouseUp(object sender, MouseEventArgs e) {
            // �I��͈̓h���b�O�I��
            Cursor.Current = Cursors.Default;

            _mouseInfo.EndDrag();
        }

        #endregion MainForm_MouseUp

        #region MainForm_MouseMove

        /// <summary>
        /// �}�E�X�J�[�\���ړ����ɌĂ΂�܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_MouseMove(object sender, MouseEventArgs e) {

            // �h���b�O�J�n�O�͏\���ɂ���
            if (_mouseInfo.Status == MouseDragInfo.DragStaus.Off)
            {
                this.Refresh();
                DrawCross(e.Location);
            } else if (_mouseInfo.Status == MouseDragInfo.DragStaus.On)
            {
                // �I��͈͂�`�悷��
                _mouseInfo.CalcDragRectangle(e.Location);

                this.Refresh();
                DrawSelectedArea(_mouseInfo.DragRectangle);
            }
        }

        /// <summary>
        /// �w�肳�ꂽ�ʒu�𒆐S�ɂ����\����`�悵�܂��B
        /// </summary>
        /// <param name="centerPoint">���S�̈ʒu</param>
        private void DrawCross(Point centerPoint) {
            using (Graphics g = this.CreateGraphics())
            {
                if (Screen.PrimaryScreen is null)
                    throw new Exception("Screen.PrimaryScreen is null");

                Rectangle rect = Screen.PrimaryScreen.WorkingArea;

                // ������
                g.DrawLine(
                    Pens.Red,
                    rect.Left,
                    centerPoint.Y,
                    rect.Right,
                    centerPoint.Y);

                // ������
                g.DrawLine(
                    Pens.Red,
                    centerPoint.X,
                    rect.Top,
                    centerPoint.X,
                    rect.Bottom);
            }
        }

        /// <summary>
        /// �I��͈͂�`�悵�܂��B
        /// </summary>
        /// <param name="rect"></param>
        private void DrawSelectedArea(Rectangle rect) {
            using (Graphics g = this.CreateGraphics())
            {
                g.DrawRectangle(Pens.Red, rect);
            }
        }

        #endregion MainForm_MouseMove

        #endregion �t�H�[���̃C�x���g�n���h��

        #region MouseDownEventHandler

        /// <summary>
        /// �}�E�X���N���b�N���ꂽ���ɌĂ΂�܂��B
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
                // �h���b�O�J�n
                _mouseInfo.BeginDrag(e.Location);

            } else if (_mouseInfo.Status == MouseDragInfo.DragStaus.Complete)
            {
                // TODO: �N���b�v�{�[�h�Ɏ�荞��
                //SetImageToClip();
                //this.Close();
            }
        }

        #region SetImageToClip

        /// <summary>
        /// �I��͈͂̃C���[�W�����N���b�v�{�[�h�ɃR�s�[���܂��B
        /// </summary>
        private void SetImageToClip() {
            // �Ԙg������
            this.Refresh();

            // �I��͈͂��N���b�v�{�[�h�ɃR�s�[
            Image img = GetCaptureImage(_mouseInfo.DragRectangle);

            // �{����ύX
            Image newImg = MagnifyImage(img, _config.Magnification);
            img.Dispose();

            // �摜�`����ϊ�
            Image newImg2 = ConvertImage(newImg);
            newImg.Dispose();

            Clipboard.SetImage(newImg2);
        }

        #endregion SetImageToClip

        #endregion MouseDownEventHandler


        #region �R���e�L�X�g���j���[

        /// <summary>
        /// �ݒ胁�j���[���N���b�N���ꂽ���ɌĂ΂�܂��B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void �ݒ�ToolStripMenuItem_Click(object sender, EventArgs e) {
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
            // �N���b�v�{�[�h�Ɏ�荞��
            SetImageToClip();
            this.Close();
        }

        #endregion �R���e�L�X�g���j���[

        #region �摜����

        #region ConvertImage

        /// <summary>
        /// �摜�̌`����ϊ����܂��B
        /// </summary>
        /// <param name="source">�ϊ�����摜</param>
        /// <returns>�ϊ����ꂽ�摜</returns>
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
        /// �w��͈͂��L���v�`�������摜���擾���܂��B
        /// </summary>
        /// <param name="rect">�L���v�`������͈�</param>
        /// <returns>�摜</returns>
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
        /// �摜�̔{����ύX���܂��B
        /// </summary>
        /// <param name="img">�摜</param>
        /// <param name="magnification">�{��</param>
        /// <returns>�ύX���ꂽ�摜</returns>
        private Image MagnifyImage(Image img, decimal magnification) {

            // TODO:���t�@�N�^����
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

        #endregion �摜����

        #region �C���i�[�N���X

        /// <summary>
        /// �ݒ�
        /// </summary>
        private class Config {

            /// <summary>
            /// �摜�̌`��
            /// </summary>
            public string ImageType;

            /// <summary>
            /// �摜�̔{��
            /// </summary>
            public decimal Magnification;
        }

        #endregion �C���i�[�N���X
    }
}