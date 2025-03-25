namespace SistemaConsultaMedica.Models.Entities
{
    public class Medico
    {
        public int Id { get; set; }
        public string CRM { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;
    }
}
