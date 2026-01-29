[Русская версия](README.md) 🇷🇺

# ModSync 🎮

**Automatic Minecraft Mods Synchronization with HTTP Server**

ModSync is a utility for automatic synchronization of Minecraft mods between server and client. The program downloads new mods from an HTTP server and removes outdated ones, ensuring your modpack stays up-to-date.

---

## ✨ Features

- 🔄 **Automatic synchronization** - download new mods from HTTP server
- 🗑️ **Remove outdated mods** - automatically detect and remove mods that are not on the server
- 📦 **Simple configuration** - JSON config with server address
- 🖥️ **Graphical folder selection** - convenient dialog for choosing Minecraft directory
- 📊 **Detailed logging** - track synchronization process with Serilog
- ⚡ **Asynchronous downloads** - fast file downloads

---

## 🚀 Quick Start

### Requirements

- .NET 6.0 or higher
- Windows (uses Windows Forms for folder selection)
- HTTP server with mods (e.g., `https://example.com/mods`)

### Installation

1. Download the latest release from [Releases](../../releases) section
2. Extract the archive to any folder
3. Edit the `config.json` file, specifying the server address with mods

### Configuration

Open the `config.json` file and change the `Server` parameter:

```json
{
  "ModFolder": "",
  "Server": "https://example.com/mods"
}
```

**Parameters:**
- `ModFolder` - path to Minecraft mods folder (leave empty to select via dialog on first run)
- `Server` - **URL of the server with mods** (must return an HTML page with links to .jar files)

> ⚠️ **Important:** Specify the URL of the server from which mods will be downloaded (e.g., `https://example.com/mods`)

---

## 💻 Usage

1. Run `ModSync.exe`
2. On first launch, select the Minecraft mods folder through the dialog window
3. The program will automatically:
   - Scan the specified HTTP server
   - Find all available .jar files (mods)
   - Compare them with local mods
   - Download new mods
   - Remove outdated mods

### Example Output

```
═══════════════════════════════
   Mod Sync Client v1.0
   By TehnoW1zard
═══════════════════════════════

🔍 Scanning server...
📦 Found mods on server: 15
💾 Local mods: 12
⬇️  Downloading jei-1.19.2.jar...
   ✓ Saved: jei-1.19.2.jar
✅ optifine-1.19.2.jar already exists
🗑️  Removing outdated mod old-mod.jar...

✅ Synchronization complete!
```

---

## 🔧 Server Setup

ModSync works with any HTTP server that provides a file list in HTML format. The program parses the HTML page and searches for all links to `.jar` files.

### Example Server Structure

```
https://example.com/mods/
├── jei-1.19.2.jar
├── optifine-1.19.2.jar
├── journeymap-1.19.2.jar
└── ...
```

### Supported Server Types

- ✅ Apache Directory Listing
- ✅ Nginx autoindex
- ✅ Any static file server with HTML listing
- ✅ Simple HTML page with links to .jar files

---

## 📝 Roadmap

- [ ] Support for other games and launchers
- [ ] GUI interface
- [ ] FTP/SFTP server support
- [ ] Hash sum verification for file validation
- [ ] Automatic launch before game start
- [ ] Multi-threaded file downloads

---

## 🤝 Contributing

Contributions are welcome! Feel free to submit pull requests and suggestions!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## 👤 Author

**TechnoW1zard**

- GitHub: [@TechnoW1zard](https://github.com/TechnoW1zard)

---

## ⭐ Support the Project

If this project helped you, please give it a star ⭐ on GitHub!

---

## 🐛 Found a Bug?

Create an [Issue](../../issues/new) with a description of the problem.

---
