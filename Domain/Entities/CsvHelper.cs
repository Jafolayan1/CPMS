using IndexAttribute = CsvHelper.Configuration.Attributes.IndexAttribute;

namespace Domain.Entities
{
    public class Account
    {
        [Index(0)]
        public string SerialNo { get; set; } = string.Empty;

        [Index(1)]
        public string FullName { get; set; } = string.Empty;

        [Index(2)]

        public string PhoneNo { get; set; } = string.Empty;
        [Index(3)]
        public string FileNo { get; set; } = string.Empty;
    }
}
