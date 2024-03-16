#!/bin/bash

dotnet ef migrations remove --context MainDbContext --project Infra --startup-project Infra
