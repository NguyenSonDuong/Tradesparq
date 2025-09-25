using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DabacoModel.modeldto
{
    /// <summary>
    /// Lớp chứa các thông tin message lỗi, tất cả các message lỗi sẽ phải được thêm vào đây
    /// để dễ dàng trong quá trình debug, dễn dàng chỉnh sửa, đồng bộ trên toàn bộ project
    /// nếu có bất cứ message có nghữi nghĩa gần giống nhau phải chú thuics
    /// </summary>
    public class MessageErrorModel
    {
        /// <summary>
        /// Khi nhận được lỗi trên có 2 nguyên nhân:
        /// 1: Lỗi do dây dẫn hoặc đường truyền chập trời dẫn đến mất đi 1 hoặc nhiều bytes dữ liệu
        /// 2: Trong quá trình lọc dữ liệu ở hàm GetArrayByteFrame bị lỗi, Login đã có lỗ hổng
        /// 3: Cân bị lỗi ( Rất ít khả năng xảy ra )
        /// CÁCH KHẮC PHỤC:
        /// 1: Khởi động lại APP và cân rồi connect lại ( Hãy thử lại 2-3 lần )
        /// 2: Debug lại quá trình lấy dữ liệu
        ///     *CHÚ Ý:
        ///     Kiểm tra byte bắt đầu [0] 0x02 và byte kết thúc [11] 0x03 vì rất có thể lỗi ở đây
        /// </summary>
        public readonly static string InvalidFrameLengthError = "Khung dữ liệu không hợp lệ. Phải có đúng 12 byte! Vui lòng liên hệ đội kỹ thuật"; 

        /// <summary>
        /// Khi nhận được lỗi trên là do:
        /// 1: Khung Frame đang bị lỗi định dạng thiếu mất byte đầu 0x02 hoặc thiếu mất Byte cuối 0x03
        /// hoặc thiếu cả 2 byte
        /// 2:
        /// </summary>
        public readonly static string InvalidFrameFormat = "Khung dữ liệu không hợp lệ. Định dạng khung nhận về không hợp lệ! Vui lòng liên hệ đội kỹ thuật";

        /// <summary>
        /// Khi nhận được lỗi trên là do:
        /// Cân đang không ở trạng thái Zero 
        /// ( Nói đến trạng thái cân bằng 0 khi vật chứa/ bệ chứa đồ trên cân bị nhấc ra)
        /// Hãy cài cân về Zero
        /// </summary>
        public readonly static string ScaleNotZeroState = "Cân đang không ở trạng thái zero! Hãy điều chỉnh cân về trạng thái Zero";

        /// <summary>
        /// Khi nhận được lỗi trên là do:
        /// Khi kết nối tới cân bị lỗi port, cổng comn ...
        /// Xem các mã lỗi sau để biết chi tiết
        /// </summary>
        public readonly static string NotConnectWeight = "Chưa kết nối với cân thành công! Vui lòng khởi động lại app và cân để kết nối loại";

        /// <summary>
        /// Lỗi xaỷ ra khi có cổng COM đang bị một ứng dụng khác kết nối đến
        /// Cách khắc phục:
        /// Hãy chuyển cổng hoặc rút ra cắm lại cổng rồi thử lại
        /// </summary>
        public readonly static string AccessDeniedToPort = "Không có quyền truy cập hoặc cổng COM đã bị chiếm dụng! Vui lòng kiểm tra lại";

        /// <summary>
        /// Lỗi xảy ra khi config kết nối tới SerialPort bị thiếu hoặc sai
        /// Cách khắc phục:
        /// Hãy kiểm tra lại File config của app
        /// </summary>
        public readonly static string InvalidSerialPortConfig = "Giá trị thiết lập cho SerialPort không hợp lệ (ví dụ: BaudRate ≤ 0, Timeout < 0, v.v.).! Vui lòng tắt app và thử lại";

        /// <summary>
        /// Lỗi xảy ra khi Tên cổng COM bị sai định dạng
        /// Cách khắc phục:
        /// Hãy kiểm tra lại File config của app
        /// </summary>
        public readonly static string UnsupportedPortName = "Tên cổng không đúng định dạng (không bắt đầu bằng \"COM\") hoặc không được hỗ trợ.! Vui lòng tắt app và thử lại";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PortConfigurationFailed = "Cổng COM đang ở trạng thái không hợp lệ hoặc không thể thiết lập cấu hình.";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string DuplicatePortOpenAttempt = "Cổng COM đã được mở trước đó trong ứng dụng hiện tại.! Vui lòng tắt app và thử lại";
        public readonly static string PortEmpry = "Công COM bị thiếu hoặc không tồn tại";
        public readonly static string FrameLostData = "Khung dữ liệu bị thiếu vui lòng kiểm tra lại";
        #region Đây là message lỗi cho các responsive
        public readonly static string ResponsiveErrorUnauthorized = "Token đã hết hạn hoặc chưa đăng nhập! Vui lòng đăng nhập lại";
        public readonly static string ResponsiveErrorForbidden = "Tài khoản không có quyền với chức năng này! Vui lòng đăng nhập lại";
        public readonly static string ResponsiveErrorNotFound = "Đường dẫn hoặc tài nguyên không tồn tại! Vui lòng kiểm tra lại";
        public readonly static string ResponsiveErrorInternalServerError = "Lỗi chưa xác định";
        public readonly static string ResponsiveErrorServiceUnavailable = "Server đang bảo trì! Vui lòng thử lại sau";
        public readonly static string ResponsiveErrorValidation = "Dữ liệu gửi lên đang thiếu! Vui lòng kiểm tra lại";
        public readonly static string ResponsiveError = "Lỗi chưa xác định? Liên hệ hỗ trợ";
        public readonly static string RequestEmtyOrError = "Dữ liệu gửi lên máy chủ bị thiếu/lỗi! Vui lòng kiểm tra lại";
        public readonly static string RequestConnectServerError = "Kết thể nối tới máy chủ ([HOST]:[POST])! liên hệ kỹ thuật viên để kiểm tra";
        public readonly static string RequestMethodNotFound = "Method chưa được hỗ trợ Vui lòng chọn Method khác";
        public readonly static string RequestUrlErrorOrConvertUrlError = "Đường dẫn hoặc quá trình chuyển đổi đường dẫn lỗi, Vui lòng kiểm tra lại";
        public readonly static string ResponsiveDataNullError = "Dữ liệu nhận về đang trống! Vui lòng liên hệ kỹ thuật viên";
        

        #endregion

        #region Các looiexc liên quan đến máy in
        public readonly static string PrinterConnectError = "Không kết nối được tới máy in! Vui lòng kiểm tra thông tin";
        public readonly static string PrinterConnectEmpty = "Không tìm thấy kết nối tới máy in! Vui lòng kiểm tra và liên hệ kỹ thuật viên";
        public readonly static string PrinterFilePRNNotFound = "File in tem mẻ không tồn tại! Vui lòng kiểm tra lại";
        public readonly static string PrinterSendFileLabelError = "Lỗi trong quá trình gửi file! Vui lòng kiểm tra kết nối";
        public readonly static string PrinterFileTemplateNotFound = "File mẫu in tem không tồn tại! Vui lòng kiểm tra lại";
        public readonly static string PrinterFilePRNWriteError = "Lưu file in tem mẻ lỗi! Vui lòng kiểm tra và thử lại";

        #endregion

        #region các lỗi liên quan đén MQTT
        public static readonly string MQTTConnectError = "Kết nối tới máy chủ MQTT bị lỗi! Vui lòng kiểm tra lại";
        public static readonly string MQTTSendMessageError = "Gửi dữ liệu lên Topic bị lỗi! Vui lòng kiểm tra lại kết nối";
        public static readonly string MQTTArguemtEmpty = "Cấu hình MQTT đang bị thiếu URI, Username hoặc Password! Vui lòng kiểm tra lại";
        
        #endregion

        #region Các lỗi liên quan đến WeightViewModel
        public static readonly string VMMQTTConfigNotFoundError = "Dữ liệu cấu hình MQTT không tồn tại! Vui lòng kiểm tra lại";
        public static readonly string VMMQTTArgumentNullError = "Dữ liệu vào đang bị trống! Vui loàng kiểm tra và bổ sung";
        public static readonly string VMMQTTTopicNullError = "Thông tin Topic đang bị trống! Vui lòng kiểm tra lại";
        public static readonly string VMMQTTCodeNVLError = "Trùng mã CODE NVL! Vui lòng kiểm tra lại";


        #endregion

        #region Các lỗi liên nquan đến ViewModel chung
        public readonly static string CreateFileError = "Không thể tạo file với đường dẫn! Vui lòng kiểm tra lại";
        public readonly static string CreateFolderError = "Không thể tạo file với đường dẫn! Vui lòng kiểm tra lại";

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public readonly static string UnknowError = "Lỗi chưa xác định";



    }
}
