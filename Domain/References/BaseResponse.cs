using static Domain.Enums.EnumType;

namespace Domain.References
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Message = string.Empty;
            Code = 0;
            MessageType = MessageType.None;
        }

        public int Code { get; set; }
        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public T Data { get; set; }

    }
}
