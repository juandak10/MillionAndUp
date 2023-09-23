using static Domain.Enums.EnumType;

namespace Domain.References
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Message = string.Empty;
            MessageCode = MessageCode.None;
            MessageType = MessageType.None;
            Token = string.Empty;
        }

        public MessageCode MessageCode { get; set; }

        public MessageType MessageType { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public T Data { get; set; }

    }
}
