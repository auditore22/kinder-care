using kinder_care.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Services;

public class ExpedienteService(KinderCareContext context)
{
    public async Task<List<ExpedienteCompletoNino>> GetExpedientesAsync()
    {
        return await context.VwExpedienteCompletoNino
            .Select(e => new ExpedienteCompletoNino
            {
                IdNino = e.IdNino,
                Cedula = e.Cedula,
                NombreNino = e.NombreNino,
                FechaNacimiento = e.FechaNacimiento,
                Direccion = e.Direccion,
                Poliza = e.Poliza,
                NombreAlergia = e.NombreAlergia,
                NombreCondicion = e.NombreCondicion,
                NombreMedicamento = e.NombreMedicamento,
                Dosis = e.Dosis,
                NombreContacto = e.NombreContacto,
                TelefonoContacto = e.TelefonoContacto,
                RelacionContacto = e.RelacionContacto
            }).ToListAsync();
    }

    public async Task<List<ExpedienteCompletoNino>> GetExpedientesByNinoIdsAsync(List<int> ninoIds)
    {
        return await context.VwExpedienteCompletoNino
            .Where(e => ninoIds.Contains(e.IdNino))
            .Select(e => new ExpedienteCompletoNino
            {
                IdNino = e.IdNino,
                Cedula = e.Cedula,
                NombreNino = e.NombreNino,
                FechaNacimiento = e.FechaNacimiento,
                Direccion = e.Direccion,
                Poliza = e.Poliza,
                NombreAlergia = e.NombreAlergia,
                NombreCondicion = e.NombreCondicion,
                NombreMedicamento = e.NombreMedicamento,
                Dosis = e.Dosis,
                NombreContacto = e.NombreContacto,
                TelefonoContacto = e.TelefonoContacto,
                RelacionContacto = e.RelacionContacto
            }).ToListAsync();
    }

    public async Task UpdateExpedienteAsync(ExpedienteCompletoNino expediente)
    {
        await context.Database.ExecuteSqlRawAsync(
            "EXEC ActualizarExpedienteNino @id_nino, @nombre_nino, @direccion, @poliza",
            new SqlParameter("@id_nino", expediente.IdNino),
            new SqlParameter("@nombre_nino", expediente.NombreNino ?? (object)DBNull.Value),
            new SqlParameter("@direccion", expediente.Direccion ?? (object)DBNull.Value),
            new SqlParameter("@poliza", expediente.Poliza ?? (object)DBNull.Value)
        );
    }

    public async Task GestionarInformacionMedicaAsync(InformacionMedicaRequest request)
    {
        await context.Database.ExecuteSqlRawAsync(
            "EXEC GestionarInformacionMedicaNino @id_nino, @id_alergia, @id_condicion, @id_medicamento, @accion",
            new SqlParameter("@id_nino", request.IdNino),
            new SqlParameter("@id_alergia", request.IdAlergia ?? (object)DBNull.Value),
            new SqlParameter("@id_condicion", request.IdCondicion ?? (object)DBNull.Value),
            new SqlParameter("@id_medicamento", request.IdMedicamento ?? (object)DBNull.Value),
            new SqlParameter("@accion", request.Accion)
        );
    }

    public async Task GestionarContactosEmergenciaAsync(ContactoEmergenciaRequest request)
    {
        await context.Database.ExecuteSqlRawAsync(
            "EXEC GestionarContactosEmergencia @id_nino, @nombre_contacto, @telefono, @relacion, @id_contacto, @accion",
            new SqlParameter("@id_nino", request.IdNino),
            new SqlParameter("@nombre_contacto", request.NombreContacto ?? (object)DBNull.Value),
            new SqlParameter("@telefono", request.Telefono ?? (object)DBNull.Value),
            new SqlParameter("@relacion", request.Relacion ?? (object)DBNull.Value),
            new SqlParameter("@id_contacto", request.IdContacto ?? (object)DBNull.Value),
            new SqlParameter("@accion", request.Accion)
        );
    }
}