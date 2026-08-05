# Ticketing System — Practice Project

This is our starting point. Right now it's just the default Blazor template with the sample pages still in it.

**Your job at this stage is to break it.** Change things, see what happens, undo it if you hate it. Nothing here is precious — there's no database and no real data, so there is genuinely nothing you can ruin. We'll start building the actual ticketing system once everyone's comfortable moving around the project.

If you've never used Git before, that's expected. Section 2 starts from zero.

---

# Part 1 — Install the tools

- **.NET 10 SDK** — https://dotnet.microsoft.com/download
- **Visual Studio 2026** (Community edition is free) or **VS Code** with the C# Dev Kit extension
- **Git** — https://git-scm.com/downloads
  - On Windows, click through the installer with all the defaults. Don't worry about the options screens.
  - On Mac, open Terminal and type `git --version`. If it's not installed it'll offer to install it for you.

Once those are done, open a terminal and check both:

```bash
dotnet --version
git --version
```

`dotnet` should print something starting with `10.` and `git` should print a version number. If either says "command not found," restart your terminal first — installers don't always update an already-open one.

> **"Open a terminal"** means Command Prompt or PowerShell on Windows, or Terminal on Mac. In Visual Studio you can also use **View → Terminal**. Anywhere you see a code block like the one above, you're typing it into that.

---

# Part 2 — Git from absolute zero

## What Git actually is

Git records snapshots of your project over time. Every time you save a snapshot, it remembers what the files looked like at that moment and who changed what. That means:

- you can undo anything, including things you did last week
- you can try something risky without fear, because the working version is still saved
- several people can work on the same project without overwriting each other

The project lives in two places: on the server (that's the shared copy everyone shares) and on your computer (your personal copy). Git's job is keeping those in sync when you want them synced.

## Words you'll see constantly

| Word | What it means |
|---|---|
| **repository** (repo) | The project folder, plus its entire history |
| **clone** | Download a copy of the repo to your computer |
| **commit** | Save a snapshot, with a note about what you changed |
| **branch** | Your own parallel version, so your work doesn't disturb anyone else's |
| **push** | Send your commits up to the server |
| **pull** | Get everyone else's commits down to your computer |
| **main** | The shared branch. The "official" version of the project |

## One-time setup

Git stamps your name on every snapshot you save, so it needs to know who you are. Run these once, ever — not per project:

```bash
git config --global user.name "Your Name"
git config --global user.email "your@email.com"
```

Use the same email as your account on the site where our repo lives.

Check it worked:

```bash
git config --global --list
```

## Getting the project onto your computer

First, `cd` into wherever you want the project to live. `cd` means "change directory" — it's how you move around in a terminal.

```bash
cd Documents
```

Then clone:

```bash
git clone <REPO_URL>
```

This creates a new folder containing the project. Move into it:

```bash
cd <PROJECT_FOLDER>
```

**The first time you do anything that touches the server, it'll ask you to log in.** A browser window will pop open — sign in there and it'll remember you afterward. If it asks for a password in the terminal instead of opening a browser, stop and message me, because the answer isn't your account password and it's a whole thing.

## The command you'll use most

```bash
git status
```

This tells you where you are, what you've changed, and what Git thinks you should do next. It's safe, it changes nothing, and it's the answer to "wait, what's going on." Run it constantly. Seriously.

---

# Part 3 — Run the project

```bash
dotnet run
```

Or open the `.sln` file in Visual Studio and press F5.

Your browser opens and you should see a home page with a nav menu containing **Counter** and **Weather**. That's the whole app right now.

To stop it: `Ctrl+C` in the terminal, or the stop button in Visual Studio.

---

# Part 4 — Make your own branch (do this before you touch anything)

**This one is not optional and it is not busywork.** If everyone edits `main` directly, we all overwrite each other's work and someone loses an evening. Your branch is your own sandbox.

## Why a branch

`main` is the shared official version. A branch is your personal copy of it that you can wreck freely. Your commits go onto your branch and nobody else sees them until we decide to merge. If you completely destroy your branch, we delete it and make a new one from `main` — no harm done.

## Make it

Right after you clone, before you edit a single file:

```bash
git checkout -b yourname/homework
```

`checkout -b` means "create a new branch and switch onto it." Use your actual name — `bob/homework`, `jen/homework` — so we can tell whose is whose.

## Confirm you're actually on it

```bash
git branch
```

You'll get a list of branches with a `*` next to the one you're on. The `*` needs to be next to yours, not `main`:

```
  main
* bob/homework
```

**Check this every single time you sit down to work.** It takes two seconds and it's the difference between a clean push and an annoying cleanup. `git status` tells you the same thing on its first line.

## Switching around later

```bash
git checkout main          # go back to the shared version
git checkout bob/homework  # return to yours
```

Your files on disk physically change when you do this — that's normal, it's Git swapping in whichever version belongs to that branch. Commit before switching, or Git will complain.

---

# Part 5 — What's in here

```
Components/
  Layout/
    MainLayout.razor     The shell — everything renders inside this
    NavMenu.razor        The sidebar links
  Pages/
    Home.razor           The landing page
    Counter.razor        A button that increments a number
    Weather.razor        A table of fake data
  App.razor              The root HTML document
wwwroot/                 CSS, images, Bootstrap
Program.cs               Startup and configuration
```

**Start by reading `Weather.razor`.** I've commented it line by line — what the `@code` block is, what the model class does, how the loop builds the table rows. It covers most of what you'll need. `Counter.razor` is the simplest possible example of a click handler if you want something smaller first.

---

# Part 6 — Things to try

Work through these in whatever order looks interesting.

## Get a feel for Razor

- Change the number Counter adds per click. Then make it subtract.
- Add a reset button to Counter.
- Add a column to the Weather table — day of the week, or a "is it freezing" yes/no.
- Generate 10 forecasts instead of 5.
- Make the temperature show in red when it's below zero. (Hint: C# works inside attributes — `class="@(temp < 0 ? "text-danger" : "")"`)

## Play with Bootstrap

Bootstrap 5 already ships with this template — it's in `wwwroot/lib/bootstrap/` and already linked. Every Bootstrap class works right now with zero setup.

Docs: https://getbootstrap.com/docs/5.3/

- Turn the Weather table into a grid of cards instead.
- Add a colored badge for each summary — hot ones red, cold ones blue.
- Restyle `NavMenu.razor` so it stops looking like the default template.
- Add a button that opens a modal.
- Resize your browser narrow and fix whatever looks bad.

## Make your own page

Copy `Counter.razor`, rename it, change the `@page "/whatever"` route at the top, and add a link to it in `NavMenu.razor`. This is the single most useful thing to be able to do from memory — you'll do it constantly.

---

# Part 7 — Committing and pushing to your branch

The rhythm is always the same four steps: **check → stage → commit → push.** You'll repeat this until it's muscle memory.

### 0. Check you're on your branch first

```bash
git branch
```

`*` next to your name, not `main`. Every time. Yes, again.

### 1. See what you changed

```bash
git status
```

Changed files show up in red under "Changes not staged for commit."

### 2. Stage them

```bash
git add .
```

The `.` means "everything I changed." Staging is you telling Git *which* changes belong in this snapshot — with `.` you're saying all of them. Run `git status` again and the files turn green.

### 3. Commit

```bash
git commit -m "Made the weather table into cards"
```

That saves the snapshot **to your branch, on your computer only.** Nothing has left your machine yet — this is the part people misunderstand. Committing is not sharing.

The `-m` message is a note to future-you. "Fixed stuff" is useless in three weeks. "Added reset button to counter" isn't.

Commit often. Every time something works is a good rule. They're cheap, and small ones are far easier to undo than one giant one.

### 4. Push

```bash
git push
```

*Now* it's on the server and I can see it.

**The first push on a brand new branch will fail** with a message about no upstream branch. That's expected — Git doesn't assume where a new branch should go. It prints the exact command to fix it, which will look like:

```bash
git push --set-upstream origin yourname/homework
```

Copy-paste what Git prints. After that once, plain `git push` works forever on that branch.

### Full worked example

Start to finish, what a session looks like:

```bash
git branch                                    # confirm: * bob/homework
# ...edit Home.razor, add some Bootstrap...
git status                                    # Home.razor in red
git add .
git commit -m "Styled home page heading and added a card"
git push

# ...keep working, add a badge...
git add .
git commit -m "Added status badge to home page"
git push
```

Two commits, both on your branch, both on the server.

### What I'm expecting when you turn it in

- Your own branch, named with your name
- **At least three commits** with messages that describe what you did
- All of it pushed — `git push` run after your last commit
- Push it even if it's unfinished or broken. Especially then.

### Prefer clicking to typing?

Visual Studio does all of this — **View → Git Changes**. It shows your current branch at the top, has a box for the commit message, and buttons for Commit All and Push. Same operations, no terminal. Use whichever sticks; nobody's grading you on command-line usage.

### Did it actually work?

Open the repo in your browser and look for the branch dropdown. Your branch should be listed, and clicking it should show your commits. If it's not there, your push didn't go through — check for an error in the terminal and message me.

---

# Part 8 — When you mess up

You will. It's fine, and it's genuinely hard to permanently lose work.

**"What's going on right now?"**
```bash
git status
```

**"I broke a file and want it back the way it was."**
```bash
git restore FileName.razor
```

**"I broke everything, throw away all my changes since the last commit."**
```bash
git restore .
```
This deletes uncommitted work on purpose. That's the point of it, but there's no undo.

**"What branch am I on again?"**
```bash
git branch
```
The one with the `*` is you.

**"I want the latest from main."**
```bash
git checkout main
git pull
```

**"I committed to `main` by accident."**

Very common, completely fixable, and nothing is lost. You're on `main` with commits that should have been on your own branch. Make the branch now — it takes your commits with it:

```bash
git checkout -b yourname/homework
git push --set-upstream origin yourname/homework
```

Your commits are now on your branch. Then put `main` back how it was:

```bash
git checkout main
git reset --hard origin/main
git checkout yourname/homework
```

That last block wipes local `main` back to match the server. Only run it once your commits are safely on your branch and pushed — check the browser first if you want to be certain.

**Anything scarier than that** — merge conflicts, a wall of red text, something about HEAD being detached — just stop and message me. Don't start googling and pasting commands you don't recognize. That's how a small problem becomes a big one.

---

# Stuck?

Ask. Paste the actual error text — "it doesn't work" takes ten messages to sort out, the error message usually takes one. Screenshots are fine.

Push whatever you've got, even half-finished or broken. It's much easier to help when I can see the code.