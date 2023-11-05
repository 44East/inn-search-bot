using Get_Info_by_INN_bot.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        WorkFileChecker checker = new WorkFileChecker();
        try
        {
            checker.CheckTheWorkFiles("conf", "appsettings.json");
        }
        catch (Exception ex) 
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
        var bot = new BotContext();
        try
        {
            await bot.RunAsync();
        }
        catch (Exception ex) 
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }
}