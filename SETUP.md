# Rock RMS Local Setup Guide

This guide covers the required setup steps to get Rock RMS running locally on your machine. Some files are intentionally gitignored for security/privacy reasons but are required for the application to run.

## Prerequisites

- **Visual Studio 2022** (required for .NET Framework 4.7.2 Web Site projects; `dotnet` CLI cannot build these)
- **SQL Server LocalDB** (included with Visual Studio 2022 installation, or install separately)
- **.NET Framework 4.7.2** (included with VS 2022)

## Required Setup Files

The following files must be created locally but are gitignored by design. They contain environment-specific or sensitive configuration.

### 1. Connection Strings Configuration
**File:** `RockWeb/web.ConnectionStrings.config`

This file is gitignored because it contains database credentials. Create it with a LocalDB connection string:

```xml
<connectionStrings>
  <add name="RockContext"
    connectionString="Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=RockDB;Integrated Security=true;MultipleActiveResultSets=true"
    providerName="System.Data.SqlClient"/>
</connectionStrings>
```

**Notes:**
- `(LocalDB)\MSSQLLocalDB` is the default LocalDB instance installed with VS 2022
- `RockDB` is the database name (can be customized)
- Integrated Security uses Windows authentication (no username/password needed for LocalDB)
- MultipleActiveResultSets must be true for Entity Framework

### 2. Migration Sentinel File
**File:** `RockWeb/App_Data/Run.Migration`

Create this as an **empty file**. Rock detects this file on startup and automatically runs Entity Framework Code-First migrations to build the database schema and seed initial data.

**Notes:**
- Rock deletes this file after migrations complete
- Migrations on first run can take **5–15 minutes** (hundreds of tables + seed data)
- Do not delete this file before migrations finish; check VS Output window for migration progress
- If you need to re-run migrations in the future, recreate this file

## Getting Started

1. **Ensure LocalDB is running:**
   ```bash
   sqllocaldb start MSSQLLocalDB
   ```

2. **Open the solution in Visual Studio 2022:**
   - File → Open → Solution
   - Navigate to `Rock.sln`

3. **Create the two files above** (`web.ConnectionStrings.config` and `Run.Migration`)

4. **Build the solution:** (Ctrl+Shift+B)
   - This builds all projects including TypeScript compilation for `Rock.JavaScript.Obsidian` and its sub-projects
   - First build may take 2–3 minutes

5. **Run the application:** (F5)
   - The browser loads to `http://localhost:6229`
   - The first load will show an infinite spinner while migrations run
   - **Wait 5–15 minutes** for migrations to complete
   - Watch the VS Output window (ASP.NET output) for migration progress
   - Once complete, Rock will redirect to the setup wizard or login page

## Troubleshooting

### Infinite Loading on First Run
**Symptom:** Browser stuck on loading spinner at `http://localhost:6229`

**Solution:** This is normal behavior while migrations run. Check the VS Output window:
- Switch to "ASP.NET" output stream
- You should see Entity Framework migration logs
- If no activity after 15+ minutes, your LocalDB connection may have failed

### NullReferenceExceptions During Migrations
**Symptom:** VS breaks on NullReferenceExceptions during startup

**Solution:** These are first-chance exceptions that Rock handles internally. Either:
- Press Continue (F5) repeatedly to skip them, or
- In the exception dialog, uncheck "Break when this exception type is thrown" to disable future breaks

### LocalDB Not Available
**Symptom:** Connection timeout or "Cannot connect to database"

**Solution:**
```bash
sqllocaldb info MSSQLLocalDB          # Check if instance exists
sqllocaldb start MSSQLLocalDB         # Start the instance
sqllocaldb stop MSSQLLocalDB          # Stop it (if needed)
sqllocaldb delete MSSQLLocalDB        # Delete and recreate if corrupted
sqllocaldb create MSSQLLocalDB        # Create a fresh instance
```

### TypeScript Compilation Errors
**Symptom:** Build fails with TS5042, TS5083, or TS6310 errors in `Rock.JavaScript.Obsidian`

**Solution:** These are known compatibility issues between TypeScript 5.5+ and the original build scripts. They have been fixed in:
- `Rock.JavaScript.Obsidian/build/build-tools.js` (DeclarationBuilder)
- `Rock.JavaScript.Obsidian/Build/build-types.js`
- `Rock.JavaScript.Obsidian.Reporting/build/build-types.js`
- `Rock.JavaScript.Obsidian.Blocks/build/build-types.js`

If you see these errors, ensure these files have been patched (see the fixes in the repo history).

## Git Commits

### Commit Message Standard

This repository enforces commit message standards via the `.git/hooks/commit-msg` hook. All commits must follow one of these formats:

**Format 1: Public Release Note (with GitHub issue fix)**
```bash
git commit -m "+ (Domain) Add/Fix/Improve/Update description. (Fixes #1234)"
```

Example:
```bash
git commit -m "+ (Core) Fix TypeScript 5.5 incompatibility in build scripts. (Fixes #5678)"
```

**Format 2: Public Release Note (without issue reference)**
```bash
git commit -m "+ (Domain) Add/Fix/Improve/Update description."
```

Example:
```bash
git commit -m "+ (Core) Fix TypeScript 5.5 incompatibility in build scripts."
```

**Format 3: Internal Commit (not included in release notes)**
```bash
git commit -m "- Internal change description"
```

Example:
```bash
git commit -m "- Update local development configuration for TypeScript builds"
```

**Valid Domains:** AI, Apple TV, API, CMS, Check-in, Communication, Connection, Core, CRM, Engagement, Event, Farm, Finance, Group, Lava, LMS, Mobile, Prayer, Reminders, Reporting, Security, Workflow, Other

**Action Keywords:** Add, Fix, Improve, Update (or variants like Adds, Added, Fixes, Fixed, etc.)

**Bypass the hook** (use for personal development or commits that will be squash-merged):
```bash
git commit -m "Your message here" --no-verify
```

The `--no-verify` flag skips the commit message validation. Use sparingly for temporary commits or emergency fixes.

For complete guidelines, see: https://community.rockrms.com/developer/developer-codex/coding-standards/committing-code

## Additional Resources

- [Rock RMS Community Docs](https://community.rockrms.com/developer)
- [QuickStart Tutorial](https://community.rockrms.com/developer/quickstart-tutorials)
- Visual Studio 2022 Help (integrated in IDE)
