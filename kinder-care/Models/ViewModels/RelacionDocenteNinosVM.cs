namespace kinder_care.Models.ViewModels
{
    public class RelacionDocenteNinosVM
    {
        public int IdDocente { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public string UsuarioCedula { get; set; } = string.Empty;
        public List<Docentes> Docentes { get; set; } = new List<Docentes>();
        public List<Ninos> Ninos { get; set; } = new List<Ninos>();
    }
}