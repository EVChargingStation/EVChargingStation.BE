namespace BlindTreasure.Application.Utils;

public static class ErrorMessages
{
    #region Notificaiton Error Message

    public const string NotificaionNotFound = "Không tìm thấy thông báo.";

    #endregion

    #region Seed Data

    public const string Seed_UserHasNotBeenSeeded = "Vui lòng khởi tạo dữ liệu tài khoản người dùng trước.";

    #endregion

    #region Account Error Message

    public const string AccountNotFound = "Không tìm thấy tài khoản với email này.";
    public const string AccountInvalidRole = "Vai trò không hợp lệ.";
    public const string AccountSuspendedOrBan = "Tài khoản đã bị tạm khóa hoặc cấm sử dụng.";
    public const string AccountLocked = "Tài khoản đã bị khóa đến {0}. Vui lòng thử lại sau.";
    public const string AccountWrongPassword = "Mật khẩu không chính xác.";
    public const string AccountVerificationCodeExpired = "Mã xác thực đã hết hạn.";
    public const string AccountInvalidVerificationCode = "Mã xác thực không chính xác.";
    public const string AccountRecoveryEmailInUse = "Email khôi phục này đã được sử dụng.";
    public const string AccountAccesstokenInvalid = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.";
    public const string ExpiredRefreshtokenInvalid = "Token làm mới đã hết hạn.";
    public const string AccountInvalidRefreshToken = "Token làm mới không hợp lệ hoặc đã hết hạn.";

    public const string AccountTooManyRecoveryAttempts =
        "Quá nhiều lần thử khôi phục mật khẩu. Vui lòng thử lại sau {0}.";

    public const string AccountInvalidRecoveryCode = "Mã khôi phục không hợp lệ hoặc đã hết hạn.";
    public const string AccountNotVerified = "Email chưa được xác thực.";
    public const string AccountEmailAlreadyRegistered = "Email này đã được đăng ký.";

    public const string AccountEmailAlreadyRegistered_NoneVerified =
        "Email đã được đăng ký. Tài khoản chưa xác thực sẽ hết hạn sau 24 giờ.";

    public const string Account_CityNotSupported =
        "Thành phố không được hỗ trợ. Hiện tại chúng tôi chỉ hỗ trợ TP. Hồ Chí Minh, Bình Dương và Hà Nội.";

    public const string AccountProfileIncomplete =
        "Hồ sơ của bạn chưa hoàn thiện. Vui lòng cập nhật đầy đủ thông tin trước khi sử dụng.";

    public const string AccountInvalidEmailFormat = "Định dạng email không hợp lệ. Vui lòng kiểm tra lại.";
    public const string AccountAlreadyVerified = "Email đã được xác thực.";

    #endregion

    #region Oauth Error Message

    public const string Oauth_ClientIdMissing = "Thiếu thông tin Client ID.";
    public const string Oauth_InvalidToken = "Token Google không hợp lệ.";
    public const string Oauth_PayloadNull = "Dữ liệu từ Google bị lỗi.";
    public const string Oauth_InvalidCredential = "Thông tin đăng nhập không hợp lệ.";
    public const string Oauth_InvalidOtp = "Mã OTP không chính xác.";

    #endregion

    #region Transaction Error Message

    public const string Currency_APIFailed = "Lỗi kết nối dịch vụ tỷ giá. Vui lòng thử lại sau. Lỗi: {0}";
    public const string Currency_RateNull = "Không thể lấy tỷ giá hiện tại. Vui lòng thử lại sau.";

    #endregion

    #region Other Error Message

    public const string Jwt_InvalidToken = "Phiên đăng nhập không hợp lệ hoặc đã hết hạn.";
    public const string Jwt_RefreshTokenExpired = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.";
    public const string AuthenticationFailed = "Xác thực không thành công. Vui lòng thử lại.";
    public const string GeneratedSubdomain = "Tên miền phụ không hợp lệ. Vui lòng thử lại.";

    #endregion

    #region Stripe

    public const string StripeTransctionFail_StripeAccountNotFound = "Bạn chưa liên kết tài khoản Stripe.";
    public const string StripeSessionNotFound = "Không tìm thấy phiên thanh toán. Vui lòng thử lại.";

    #endregion

    #region Caching

    public const string VerifyOtpExistingCoolDown =
        "Bạn đang gửi mã OTP quá nhanh. Vui lòng chờ vài phút trước khi thử lại.";

    public const string CacheUserNotFound = "Không tìm thấy thông tin người dùng.";

    #endregion

    #region BlindBox Error Message

    public const string BlindBoxNotFound = "Không tìm thấy hộp bí ẩn.";
    public const string BlindBoxDataRequired = "Thông tin hộp bí ẩn là bắt buộc.";
    public const string BlindBoxNameRequired = "Tên hộp bí ẩn là bắt buộc.";
    public const string BlindBoxPriceInvalid = "Giá hộp bí ẩn phải lớn hơn 0.";
    public const string BlindBoxTotalQuantityInvalid = "Số lượng hộp bí ẩn phải lớn hơn 0.";
    public const string BlindBoxBrandRequired = "Thương hiệu là bắt buộc.";
    public const string BlindBoxReleaseDateInvalid = "Ngày phát hành không hợp lệ.";
    public const string BlindBoxImageRequired = "Ảnh hộp bí ẩn là bắt buộc.";
    public const string BlindBoxSellerNotVerified = "Bạn cần được xác thực làm người bán để tạo hộp bí ẩn.";
    public const string BlindBoxImageUrlError = "Lỗi khi tải ảnh hộp bí ẩn.";
    public const string BlindBoxNoUpdatePermission = "Bạn không có quyền cập nhật hộp bí ẩn này.";
    public const string BlindBoxImageUpdateError = "Lỗi khi cập nhật ảnh hộp bí ẩn.";
    public const string BlindBoxNoEditPermission = "Bạn không có quyền chỉnh sửa hộp bí ẩn này.";
    public const string BlindBoxItemCountInvalid = "Hộp bí ẩn phải chứa đúng 6 hoặc 12 món đồ.";
    public const string BlindBoxAtLeastOneItem = "Hộp bí ẩn phải có ít nhất một món đồ.";
    public const string BlindBoxDropRateMustBe100 = "Tổng tỷ lệ rơi phải bằng 100%.";

    public const string BlindBoxNotFoundOrNotPending =
        "Không tìm thấy hộp bí ẩn hoặc hộp không ở trạng thái chờ duyệt.";

    public const string BlindBoxNoItems = "Hộp bí ẩn không có món đồ nào.";
    public const string BlindBoxRejectReasonRequired = "Lý do từ chối là bắt buộc.";

    public const string BlindBoxNoDeleteItemPermission =
        "Bạn không có quyền xóa món đồ khỏi hộp bí ẩn này.";

    public const string BlindBoxNoDeletePermission = "Bạn không có quyền xóa hộp bí ẩn này.";
    public const string BlindBoxItemListRequired = "Danh sách món đồ không được để trống.";
    public const string BlindBoxProductInvalidOrOutOfStock = "Một hoặc nhiều sản phẩm không hợp lệ hoặc hết hàng.";
    public const string BlindBoxProductStockExceeded = "Sản phẩm '{0}' vượt quá số lượng có sẵn.";
    public const string BlindBoxNoSecretSupport = "Hộp bí ẩn này không hỗ trợ món đồ bí mật.";
    public const string BlindBoxSecretItemRequired = "Hộp bí ẩn phải có ít nhất một món đồ bí mật.";
    public const string BlindBoxDropRateExceeded = "Tổng tỷ lệ rơi (không bao gồm bí mật) phải nhỏ hơn 100%.";

    #endregion

    #region Cart Error Message

    public const string CartItemQuantityMustBeGreaterThanZero = "Số lượng phải lớn hơn 0.";
    public const string CartItemProductOrBlindBoxRequired = "Bạn phải chọn một sản phẩm hoặc hộp bí ẩn.";
    public const string CartItemProductNotFound = "Không tìm thấy sản phẩm.";
    public const string CartItemProductOutOfStock = "Sản phẩm đã hết hàng.";
    public const string CartItemBlindBoxNotFoundOrRejected = "Không tìm thấy hộp bí ẩn hoặc đã bị từ chối.";
    public const string CartItemNotFound = "Không tìm thấy sản phẩm trong giỏ hàng.";
    public const string CartItemBlindBoxNotFound = "Không tìm thấy hộp bí ẩn.";

    #endregion

    #region Category Error Message

    public const string CategoryNotFound = "Không tìm thấy danh mục.";
    public const string CategoryChildrenError = "Chỉ được chọn danh mục cấp thấp nhất (không có danh mục con).";
    public const string CategoryNameRequired = "Tên danh mục là bắt buộc.";
    public const string CategoryNameAlreadyExists = "Tên danh mục đã tồn tại.";
    public const string CategoryParentIdInvalid = "Danh mục cha không hợp lệ.";
    public const string CategoryImageOnlyForRoot = "Chỉ danh mục gốc mới có thể có ảnh.";
    public const string CategoryImageUploadError = "Lỗi khi tải ảnh danh mục.";
    public const string CategoryNoUpdatePermission = "Bạn không có quyền cập nhật danh mục.";
    public const string CategoryParentIdSelf = "Danh mục không thể làm cha của chính nó.";
    public const string CategoryHierarchyLoop = "Không thể tạo vòng lặp trong cấu trúc danh mục.";
    public const string CategoryNoDeletePermission = "Bạn không có quyền xóa danh mục.";

    public const string CategoryDeleteHasChildrenOrProducts =
        "Không thể xóa danh mục có sản phẩm hoặc danh mục con liên quan.";

    #endregion

    #region Order Error Message

    public const string OrderCartEmpty = "Giỏ hàng trống.";
    public const string OrderCartEmptyLog = "[CheckoutAsync] Giỏ hàng trống.";
    public const string OrderCheckoutStartLog = "[CheckoutAsync] Bắt đầu xử lý thanh toán từ giỏ hàng hệ thống.";
    public const string OrderClientCartInvalid = "Giỏ hàng không hợp lệ hoặc trống.";

    public const string OrderClientCartInvalidLog =
        "[CheckoutFromClientCartAsync] Giỏ hàng client không hợp lệ hoặc trống.";

    public const string OrderCheckoutFromClientStartLog =
        "[CheckoutFromClientCartAsync] Bắt đầu xử lý thanh toán từ giỏ hàng client.";

    public const string OrderCartEmptyOrInvalid = "Giỏ hàng trống hoặc không hợp lệ.";
    public const string OrderCartEmptyOrInvalidLog = "[CheckoutCore] Giỏ hàng trống hoặc không hợp lệ.";
    public const string OrderShippingAddressInvalid = "Địa chỉ giao hàng không hợp lệ hoặc không thuộc về bạn.";

    public const string OrderShippingAddressInvalidLog =
        "[CheckoutCore] Địa chỉ giao hàng không hợp lệ hoặc không thuộc về người dùng.";

    public const string OrderProductNotFound = "Không tìm thấy sản phẩm {0}.";
    public const string OrderProductOutOfStock = "Sản phẩm {0} đã hết hàng.";
    public const string OrderProductNotForSale = "Sản phẩm {0} không còn được bán.";
    public const string OrderBlindBoxNotFound = "Không tìm thấy hộp bí ẩn {0}.";
    public const string OrderBlindBoxNotApproved = "Hộp bí ẩn {0} chưa được duyệt.";
    public const string OrderBlindBoxOutOfStock = "Hộp bí ẩn {0} đã hết hàng.";

    public const string OrderCartClearedAfterCheckoutLog =
        "[CheckoutCore] Đã xóa giỏ hàng hệ thống sau khi thanh toán.";

    public const string OrderCacheClearedAfterCheckoutLog =
        "[CheckoutCore] Đã xóa cache đơn hàng cho người dùng {0} sau khi thanh toán.";

    public const string OrderCheckoutSuccessLog = "[CheckoutCore] Thanh toán thành công cho người dùng {0}.";
    public const string OrderCacheHitLog = "[GetOrderByIdAsync] Cache hit cho đơn hàng {0}";
    public const string OrderNotFoundLog = "[GetOrderByIdAsync] Không tìm thấy đơn hàng {0}.";
    public const string OrderNotFound = "Không tìm thấy đơn hàng.";
    public const string OrderLoadedAndCachedLog = "[GetOrderByIdAsync] Đã tải đơn hàng {0} từ DB và cache.";
    public const string OrderListLoadedLog = "[GetMyOrdersAsync] Đã tải danh sách đơn hàng từ DB.";

    public const string OrderNotFoundOrNotBelongToUserLog =
        "[CancelOrderAsync] Không tìm thấy đơn hàng {0} hoặc không thuộc về người dùng.";

    public const string OrderNotPendingLog = "[CancelOrderAsync] Đơn hàng {0} không ở trạng thái CHỜ XỬ LÝ.";
    public const string OrderCancelOnlyPending = "Chỉ có thể hủy đơn hàng đang ở trạng thái chờ xử lý.";

    public const string OrderCacheClearedAfterCancelLog =
        "[CancelOrderAsync] Đã xóa cache đơn hàng cho người dùng {0} sau khi hủy.";

    public const string OrderCancelSuccessLog = "[CancelOrderAsync] Đã hủy thành công đơn hàng {0}.";

    public const string OrderCacheClearedAfterDeleteLog =
        "[DeleteOrderAsync] Đã xóa cache đơn hàng cho người dùng {0} sau khi xóa.";

    public const string OrderDeleteSuccessLog = "[DeleteOrderAsync] Đã xóa thành công đơn hàng {0}.";

    #endregion

    #region Product Error Message

    public const string ProductNotFound = "Không tìm thấy sản phẩm.";
    public const string ProductNotFoundOrDeleted = "Sản phẩm không tồn tại hoặc đã bị xóa.";
    public const string ProductSellerNotFound = "Không tìm thấy người bán.";
    public const string ProductSellerNotVerified = "Người bán chưa được xác thực.";
    public const string ProductCreatedLog = "[CreateAsync] Sản phẩm {0} được tạo với {1} hình ảnh.";
    public const string ProductUpdateNotFoundLog = "[UpdateAsync] Không tìm thấy sản phẩm {0} hoặc đã bị xóa.";
    public const string ProductUpdateLog = "[UpdateAsync] Người dùng {0} cập nhật sản phẩm {1}";
    public const string ProductUpdateSuccessLog = "[UpdateAsync] Sản phẩm {0} được cập nhật bởi người dùng {1}";
    public const string ProductDeleteNotFoundLog = "[DeleteAsync] Không tìm thấy sản phẩm {0} hoặc đã bị xóa.";
    public const string ProductDeleteSuccessLog = "[DeleteAsync] Sản phẩm {0} được xóa mềm bởi người dùng {1}";
    public const string ProductImageFileInvalidLog = "[UploadProductImageAsync] File ảnh không hợp lệ hoặc trống.";
    public const string ProductImageFileInvalid = "File ảnh không hợp lệ hoặc trống.";

    public const string ProductImageNotFoundLog =
        "[UploadProductImageAsync] Không tìm thấy sản phẩm {0} hoặc đã bị xóa.";

    public const string ProductImageUrlErrorLog = "[UploadProductImageAsync] Không thể tạo URL cho file {0}";
    public const string ProductImageUrlError = "Không thể tạo URL ảnh.";
    public const string ProductImageUploadingLog = "[UploadProductImageAsync] Đang tải file: {0}";

    public const string ProductImageUpdateSuccessLog =
        "[UploadProductImageAsync] Đã cập nhật ảnh cho sản phẩm {0}: {1}";

    #endregion
}