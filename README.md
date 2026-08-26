# Super Simple Ticketing System

Homework 2 adds a real database. You'll pull down the new code, point it at the database with your own credentials, and then write your own version of what's already there.

---

# Part 1 — Push what you've got

**Before anything else, save and push your current work.** If you switch branches with uncommitted changes, Git will either drag them along with you or refuse to move, and both are annoying to untangle.

1. **View → Git Changes** (if it isn't open already)
2. Confirm the branch at the top is yours (`bob/homework`), not `main`
3. Stage everything with the **+** on the Changes header
4. Type a message and click **Commit Staged**
5. Click **Push** (the up arrow at the top of the panel)

Push even if homework 1 is half-finished or broken. Especially then.

---

# Part 2 — Switch back to `main`

Bottom right corner of Visual Studio → click your branch name → select **main**.

Now open `Components/Pages/Home.razor`. It will look exactly like it always did — none of the new code is there.

**This is correct.** Your copy of `main` is a snapshot from whenever you cloned it. New commits went up to GitHub since then, and nothing pulls them down automatically. You have to ask for them.

---

# Part 3 — Pull the new code

With `main` checked out: **Git menu → Pull**.

You should see files change in the output. Verify it worked by opening `Components/Pages/Home.razor` — it should now have:

- `@inject Services.Dapper DapperService` at the top
- A **Click This For a List of Techs** button
- An `@code` block with a `ShowTechs()` method

If you see that, you're good. Skip to Part 4.

### If you don't see it

Work through these in order:

**Check the bottom right corner.** If it doesn't say `main`, the pull updated a different branch. Switch to `main` and pull again.

**Try Git menu → Fetch, then Pull.** Fetch checks GitHub for new commits, Pull downloads and applies them. Occasionally Visual Studio needs the nudge.

**"Cannot pull because you have uncommitted changes."** You edited files while on `main`. Open Git Changes, right-click each changed file → **Undo Changes** (this throws those edits away — that's fine, your real work is safe on your own branch), then pull again.

**Solution Explorer looks wrong — no Components folder, no Program.cs.** You're in Folder View. Click `super-simple-ticketing-system.slnx` at the top of Solution Explorer to switch to the solution.

**A wall of red text, "merge conflict," or "detached HEAD."** Stop and message me. Don't google it and start pasting commands — that's how a two-minute fix becomes an hour.

---

# Part 4 — Branch off the *updated* `main`

**Order matters here.** You pulled first so that your new branch starts from the latest code. If you'd branched before pulling, you'd be building on the old snapshot.

With `main` checked out and up to date:

1. Bottom right corner → click `main` → **New Branch**
2. Name it with your name in front:

```
bob/homework2
```

3. Leave **Based on** set to `main`
4. Make sure **Checkout branch** is ticked
5. Create

Bottom right should now show `bob/homework2`. Glance at that corner every time you sit down. If it says `main`, you're on the wrong branch.

---

# Part 5 — Add your credentials, and hide them from Git

### 1. Fill in your credentials

Open **appsettings.json**. The connection string is in there with the username and password redacted. Replace both with the ones I sent you, and leave everything else alone.

### 2. Tell Git to stop watching that file

**Do this now, before you commit anything.**

`appsettings.json` is in the repo — that's how you got the connection string in the first place. Which means Git is tracking it, and the moment you type your password into it, it'll turn up in Git Changes as a modified file. Stage everything with the **+** button and you've just pushed your password to GitHub.

One command stops that:

1. **View → Terminal**
2. Paste this and hit Enter:

```
git update-index --skip-worktree super-simple-ticketing-system/appsettings.json
```

It prints nothing when it works. That's normal — no news is good news.

What it does: tells Git *this file stays in the repo, but stop paying attention to my local changes to it.* Your credentials stay on your machine.

### 3. Confirm it worked

Open **View → Git Changes**. Even though you just edited `appsettings.json`, it should **not** appear in the Changes list.

**If it's still there, stop and message me before you commit anything.** The command didn't take, and I'd rather sort that out now than pull your password out of the history later.

### "Isn't that what .gitignore is for?"

Normally, yes. A `.gitignore` is a plain text file at the root of the repo listing things Git should pretend don't exist — build output, local settings, credentials. Ours is there; open it and have a look.

But **.gitignore only works on files Git has never tracked.** Once a file is committed, adding it to .gitignore does nothing at all — Git keeps watching it. Since I had to commit `appsettings.json` so you'd have the connection string, ignoring it isn't an option. `skip-worktree` is the tool for that other situation: a file that has to be in the repo, but whose contents are different for every person.

Worth knowing because the "why is Git still tracking this, I ignored it" confusion catches out people who've been using Git for years.

And to be clear about the real-world version: you wouldn't put credentials in a config file at all. They'd live in user secrets, environment variables, or a key vault, with nothing sensitive anywhere near the repo. We'll get to that.

---

# Part 6 — Wake the database up first

**Do this before you run the project.**

Open **SQL Server Management Studio** and connect to the database with your credentials.

**The first attempt will probably fail.** You'll get a timeout, or an error saying the database isn't currently available. This is expected and nothing is broken — the database pauses itself when nobody's used it for a while, and the failed connection attempt is what wakes it back up.

Wait a minute or two and connect again. It usually takes one or two tries.

Once SSMS connects, have a look around: expand **Databases → Tables**. You'll see `Technicians`, `TicketStatus`, and `TicketType`. Right-click a table → **Select Top 1000 Rows** to see what's actually in it. Knowing what the data looks like makes the next part much easier.

Now run the project. If you skip the SSMS step and go straight to running it, the page will just error out while the database is still waking up.

---

# Part 7 — The assignment

`Home.razor` currently has one working example: a button that fetches technicians and lists their first names.

```razor
@inject Services.Dapper DapperService
@rendermode InteractiveServer
@page "/"
@using super_simple_ticketing_system.Models

<PageTitle>Home</PageTitle>

<h1>Hello, world!</h1>

Welcome to your new app. Click on the "Homework" link in the navigation menu to get started.

<button @onclick="ShowTechs">Click This For a List of Techs</button>

@foreach (var tech in technicians)
{
    <p>@tech.FirstName</p>
}

@code {
    List<Technicians> technicians = new List<Technicians>();

    private async Task ShowTechs()
    {
        technicians = (await DapperService.GetTechniciansAsync()).ToList();
    }
}
```

**Your job:** underneath the technicians loop, add the same thing twice more — one button that lists **TicketStatus**, and one button that lists **TicketType**. Each gets its own button, its own list, and its own method.

Read the existing example closely and copy the pattern. Everything you need is in those four pieces:

- The `@onclick` that wires a button to a method
- The list declared in `@code`
- The method that fills it from `DapperService`
- The `@foreach` that renders it

Look at `Services/Dapper.cs` to see what methods are available and what they return, and at the `Models` folder to see what properties each type has. If the method you need isn't in `Dapper.cs`, writing it is part of the assignment — `GetTechniciansAsync()` shows you the shape.

If your button appears but clicking it does nothing, check that `@rendermode InteractiveServer` is still at the top of the file.

---

# Part 8 — Turning it in

Commit every time something works. Small commits are much easier to undo than one giant one.

What I want:

- Branch named `yourname/homework2`, branched off the updated `main`
- **Pushed.** Committing saves to your computer only. Push is the step that puts it on GitHub where I can see it.
- No `appsettings.json` in any of your commits

The first push on a new branch, Visual Studio may ask you to confirm creating the branch on GitHub. Say yes.

### Did it actually work?

Go to https://github.com/lmvicente/super-simple-ticketing-system, click the branch dropdown, and look for `yourname/homework2`. Clicking it should show your commits. If it's not there, the push didn't go through — check Git Changes for an error and message me.

---

# Stuck?

Ask. Paste the actual error text — "it doesn't work" takes ten messages to sort out, the error message usually takes one. Screenshots are fine.

Push whatever you've got, even half-finished or broken. It's far easier to help when I can see the code.