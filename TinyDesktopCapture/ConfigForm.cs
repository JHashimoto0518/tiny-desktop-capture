using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyDesktopCapture.Properties;

namespace TinyDesktopCapture {
    public partial class ConfigForm : Form {

        #region プロパティ

        /// <summary>
        /// 画像の倍率
        /// </summary>
        public decimal Magnification;

        /// <summary>
        /// 画像の形式
        /// </summary>
        public string? ImageType;

        #endregion プロパティ

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigForm() {
            InitializeComponent();

            this.Icon = Resources.Settings;

            OKButton.Click += new EventHandler(OKButton_Click);
            cancelButton.Click += new EventHandler(delegate { this.Close(); });

            SetImageComboBox();

            Settings.Default.Reload();
        }

        #endregion コンストラクタ

        #region 画像形式コンボボックス

        /// <summary>
        /// 画像形式コンボボックスを設定します。
        /// </summary>
        private void SetImageComboBox() {
            imageComboBox.Items.Add("png");
            imageComboBox.Items.Add("bitmap");
            imageComboBox.Items.Add("gif");
            imageComboBox.Items.Add("jpeg");
        }

        #endregion

        #region OKボタン

        /// <summary>
        /// OKボタンが押された時に呼ばれます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OKButton_Click(object sender, EventArgs e) {
            ImageType = imageComboBox.SelectedText;
            Magnification = 倍率NumericUpDown.Value;

            Settings.Default.Save();
            this.Close();
        }

        #endregion

    }
}
