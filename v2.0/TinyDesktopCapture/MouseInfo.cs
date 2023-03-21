using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyDesktopCapture {
    /// <summary>
    /// ドラッグ情報を保持します。
    /// </summary>
    class MouseDragInfo {

        // TODO:右下から左上にドラッグすると例外

        #region Enum

        public enum DragStaus {
            Off = 1,
            On = 2,
            Complete = 3
        }

        #endregion

        #region フィールド

        /// <summary>
        /// ドラッグ開始位置
        /// </summary>
        private Point _startLocation;

        #endregion フィールド

        #region プロパティ

        private DragStaus _status;

        /// <summary>
        /// ステータス
        /// </summary>
        public DragStaus Status {
            get {
                return _status;
            }
        }

        /// <summary>
        /// ドラッグで描かれた四角形
        /// </summary>
        private Rectangle _dragRectangle;

        public Rectangle DragRectangle {
            get {
                return _dragRectangle;
            }
            set {
                _dragRectangle = value;
            }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MouseDragInfo() {
            _status = DragStaus.Off;
        }

        #endregion

        #region BeginDrag

        /// <summary>
        /// ドラッグを開始する時に呼び出します。
        /// </summary>
        /// <param name="location"></param>
        public void BeginDrag(Point location) {
            _startLocation = location;

            _status = DragStaus.On;
        }

        #endregion BeginDrag

        #region EndDrag

        /// <summary>
        /// ドラッグを終了する時に呼び出します。
        /// </summary>
        public void EndDrag() {
            _status = DragStaus.Complete;
        }

        #endregion EndDrag

        #region CalcDragRectangle

        /// <summary>
        /// ドラッグ開始位置と指定位置とを頂点とする四角形を算出します。
        /// </summary>
        /// <param name="location">ドラッグ開始位置と対になる頂点</param>
        public void CalcDragRectangle(Point location) {
            _dragRectangle = Rectangle.FromLTRB(
                                _startLocation.X,
                                _startLocation.Y,
                                location.X,
                                location.Y);
        }

        #endregion CalcDragRectangle

    }
}
