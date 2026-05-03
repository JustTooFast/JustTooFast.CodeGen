# Agent Rules

## Source Control

The agent may create and modify files as instructed by transformations.

The agent must not commit code.

The agent must not push code.

The agent must not create branches unless explicitly instructed.

The agent must not stage files with `git add`.

The agent must leave all changes in the working tree for human review.

## Allowed Commands

The agent may run:

    dotnet build
    dotnet test
    git status
    git diff
    git diff --stat

## Forbidden Commands

The agent must not run:

    git add
    git commit
    git push
    git checkout
    git switch
    git merge
    git rebase
    git reset
    git clean