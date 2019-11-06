using System.Runtime.Serialization;

namespace AdvertService.Models
{
    [DataContract]
    public class AdvertType
    {
        [DataMember]
        public int ImageId { get; set; }

        [DataMember]
        public byte[] ImageByteArray { get; set; }
    }
}