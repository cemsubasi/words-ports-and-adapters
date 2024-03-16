#!/bin/bash

dotnet ef migrations add $1 --context MainDbContext --project Infra --startup-project Infra --output-dir src/main/Context/Migrations --json
