using System.Drawing;
using System.Windows.Forms;

namespace TinyDesktopCapture {

    /// <summary>
    /// マウスの左ボタンによるドラッグでフォームを移動可能にします。
    /// </summary>
    public class DraggableExtender {

        #region フィールド

        /// <summary>
        /// 移動可能にするフォーム
        /// </summary>
        private Form targerForm;

        /// <summary>
        /// マウスの左ボタンでドラッグ中の場合はtrue。それ以外はfalse。
        /// </summary>
        private bool isLeftDrag;

        /// <summary>
        /// ドラッグ開始時のマウスカーソル位置
        /// </summary>
        private Point cursolOffset;

        #endregion フィールド

        #region プロパティ

        /// <summary>
        /// ドラッグで移動させる場合はtrue。移動させない場合はfalse。
        /// </summary>
        public bool Enabled {
            get {
                return this.enabled;
            }
            set {
                this.enabled = value;
            }
        }

        private bool enabled = true;

        #endregion プロパティ

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="form">移動可能にするフォーム</param>
        public DraggableExtender(Form form) {
            this.targerForm = form;

            // マウスカーソルの移動距離計測を開始します。
            this.targerForm.MouseDown += (sender, e) => {
                if (!this.enabled) { return; }

                if (e.Button == MouseButtons.Left)
                {
                    if (this.targerForm.FormBorderStyle == FormBorderStyle.None)
                    {
                        this.cursolOffset = new Point(-e.X, -e.Y);
                    } else
                    {
                        // マウスカーソル位置にウィンドウの境界線とタイトルバーの高さを加味する。
                        this.cursolOffset = new Point(
                            -e.X - SystemInformation.FrameBorderSize.Width,
                            -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height);
                    }

                    this.isLeftDrag = true;
                }
            };

            // マウスカーソルの移動距離の分だけフォームを移動します。
            this.targerForm.MouseMove += (sender, e) => {
                if (!this.enabled) { return; }

                if (this.isLeftDrag)
                {
                    Point p = Control.MousePosition;
                    p.Offset(this.cursolOffset);
                    this.targerForm.Location = p;
                }
            };

            // マウスカーソルの移動距離計測を終了します。
            this.targerForm.MouseUp += (sender, e) => {
                if (!this.enabled) { return; }

                if (e.Button == MouseButtons.Left)
                {
                    this.isLeftDrag = false;
                }
            };
        }
    }
}
