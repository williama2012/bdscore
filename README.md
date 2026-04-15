# bdscore

A C# / .NET solution containing a collection of utility libraries and tools for SQL, web automation, email, and Windows system tasks.

## Overview

`bdscore` is a multi-project Visual Studio solution that bundles together several reusable libraries and standalone utilities. The codebase is organized into core libraries (`bds.*`) and supporting tools.

## Projects

### Core Libraries

| Project | Description |
|---------|-------------|
| `bdscore` | Main core library |
| `bds.cr` | Shared common resources |
| `bds.sql` | SQL helpers and database utilities |
| `bds.web` | Web-related utilities |
| `bds.win` | Windows-specific utilities |
| `bds.xml` | XML parsing and serialization helpers |

### Tools & Utilities

| Project | Description |
|---------|-------------|
| `BrowserBot` | Browser automation utility |
| `SQLExecute` | Tool for executing SQL scripts |
| `SqlParser` | SQL statement parser |
| `SqlSmoSearcher` | SQL Server SMO-based search utility |
| `SmtpListerener` | SMTP listener / mail capture service |
| `WindowsTempCleaner` | Cleans temporary files on Windows |

### Tests

| Project | Description |
|---------|-------------|
| `bds.core.test` | Unit tests for the core library |
| `bds.sql.test` | Unit tests for the SQL library |

## Requirements

- Windows
- Visual Studio 2022 (or later)
- .NET Framework (per project targets)
- SQL Server (for SQL-related projects, if applicable)

## Getting Started

Clone the repository:

```bash
git clone https://github.com/williama2012/bdscore.git
cd bdscore
```

Open the solution in Visual Studio:

```
bdscore.sln
```

Restore NuGet packages and build the solution (`Build > Build Solution` or `Ctrl+Shift+B`).

## Running Tests

Tests can be run from Visual Studio's Test Explorer, or via the command line:

```bash
dotnet test
```

## Project Structure

```
bdscore/
├── bdscore.sln          # Solution file
├── bdscore/             # Main core project
├── bds.cr/              # Common resources
├── bds.sql/             # SQL utilities
├── bds.web/             # Web utilities
├── bds.win/             # Windows utilities
├── bds.xml/             # XML utilities
├── bds.core.test/       # Core tests
├── bds.sql.test/        # SQL tests
├── BrowserBot/          # Browser automation tool
├── SQLExecute/          # SQL execution tool
├── SqlParser/           # SQL parser
├── SqlSmoSearcher/      # SQL SMO search tool
├── SmtpListerener/      # SMTP listener
├── WindowsTempCleaner/  # Temp file cleaner
├── lib/                 # External libraries
└── packages/            # NuGet packages
```

## Contributing

Contributions are welcome. Please open an issue to discuss proposed changes before submitting a pull request.

## License

No license has been specified for this repository. All rights reserved by the author unless stated otherwise.

## Author

[williama2012](https://github.com/williama2012)
