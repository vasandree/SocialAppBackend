#!/bin/bash

# shellcheck disable=SC2162
read -p "Enter migration name: " MIGRATION_NAME

if [ -z "$MIGRATION_NAME" ]; then
  echo "Migration name cannot be empty."
  exit 1
fi

echo "Creating migration: $MIGRATION_NAME"
dotnet ef migrations add "$MIGRATION_NAME" --startup-project ../SocialApp.Api --context WorkspaceDbContext

echo "Updating database..."
dotnet ef database update --startup-project ../SocialApp.Api --context WorkspaceDbContext
