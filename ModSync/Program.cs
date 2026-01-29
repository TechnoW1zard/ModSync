using ModSync;
using Serilog;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace Program
{
    class ModSync
    {
        public static Config config;
        public static HttpClient _httpClient;
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            config = JsonSerializer.Deserialize<Config>(System.IO.File.ReadAllText("config.json"));
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            Console.WriteLine("═══════════════════════════════");
            Console.WriteLine("   Mod Sync Client v1.0");
            Console.WriteLine("   By TehnoW1zard");
            Console.WriteLine("═══════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("You need choose MineCraft Mods folder path");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            string path = config.ModFolder;
            if (string.IsNullOrEmpty(path))
                path = Functions.SelectFolderWithOpenDialog();


            _httpClient = new HttpClient();
            config.ModFolder = path;
            Config.SaveConfig(config);

            SyncMods();

        }


        public static void SyncMods()
        {
            Console.WriteLine("🔍 Сканирование сервера...");

            // Получаем список модов с сервера (парсим HTML)
            var serverMods = GetServerModsListAsync().Result;

            Console.WriteLine($"📦 Найдено модов на сервере: {serverMods.Count}");

            //// Создаём папку если не существует
            //Directory.CreateDirectory(config.ModFolder);3

            // Получаем локальные моды
            var localMods = Directory.GetFiles(config.ModFolder, "*.jar")
                .Select(f => Path.GetFileName(f))
                .ToHashSet();

            Log.Information("💾 Локальных модов: {Count}", localMods.Count);

            // Скачиваем новые моды
            foreach (var serverMod in serverMods)
            {
                if (!localMods.Contains(serverMod))
                {
                    Log.Information("⬇️  Скачивание {serverMod}...", serverMod);
                    DownloadModAsync(serverMod).Wait();
                }
                else
                {
                    Log.Information("✅ {serverMod} уже существует", serverMod);
                }
            }

            // Удаляем моды которых нет на сервере
            foreach (var localMod in localMods)
            {
                if (!serverMods.Contains(localMod))
                {
                    Log.Information("🗑️  Удаление устаревшего мода {localMod}...", localMod);

                    //Console.WriteLine($"🗑️  Удаление устаревшего мода {localMod}...");
                    //File.Delete(Path.Combine(config.ModFolder, localMod));
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Log.Information("✅ Синхронизация завершена!");
        }

        public static async Task<HashSet<string>> GetServerModsListAsync()
        {
            var mods = new HashSet<string>();

            try
            {
                var html = await _httpClient.GetStringAsync(config.Server);

                // Парсим HTML и ищем ссылки на .jar файлы
                var regex = new Regex(@"href=[""']([^""']*\.jar)[""']", RegexOptions.IgnoreCase);
                var matches = regex.Matches(html);

                foreach (Match match in matches)
                {
                    var fileName = match.Groups[1].Value;

                    // Убираем путь, оставляем только имя файла
                    if (fileName.Contains("/"))
                    {
                        fileName = fileName.Split('/').Last();
                    }
                    fileName = Uri.UnescapeDataString(fileName);
                    mods.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                Log.Error("❌ Ошибка получения списка: {Message}", ex.Message);
            }

            return mods;
        }

        public static async Task DownloadModAsync(string modName)
        {
            try
            {
                var url = $"{config.Server}/{modName}";
                var localPath = Path.Combine(config.ModFolder, modName);

                var data = await _httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(localPath, data);

                Log.Information("   ✓ Сохранено: {modName}", modName);
            }
            catch (Exception ex)
            {
                Log.Error("   ✗ Ошибка: {Message}", ex.Message);
            }
        }
    }
}