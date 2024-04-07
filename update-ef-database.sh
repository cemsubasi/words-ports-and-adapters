#!/bin/bash

dotnet ef database update --context MainDbContext --project Infra --startup-project Infra --verbose
