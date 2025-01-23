namespace kinder_care.Models;

public class Documentos
{
    public int IdDoc { get; set; }
    
    public byte[] Documento { get; set; } = null!;
}