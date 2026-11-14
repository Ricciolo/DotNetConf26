FROM mcr.microsoft.com/dotnet/sdk:11.0

ARG USERNAME=vscode

RUN apt-get update \
    && apt-get install -y --no-install-recommends git curl ca-certificates \
    && rm -rf /var/lib/apt/lists/* \
    && useradd --create-home --shell /bin/bash "${USERNAME}"

USER ${USERNAME}
WORKDIR /workspaces/DotNetConf26
