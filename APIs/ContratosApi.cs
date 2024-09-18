public static class ContratosApi {
    public static void MapAlunosApi(this WebApplication app) {
        var group= app.MapGroup("/contratos");

        group.MapGet("/", async (AppDataBase db) => {
            await db.Contratos.ToListAsync()
        })
    }
}