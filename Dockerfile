FROM microsoft/dotnet:2.1.300-sdk

WORKDIR /app
COPY . ./

RUN dotnet build osu.Server.sln -c Release
RUN cp -r osu.Server.DifficultyCalculator/bin/Release/netcoreapp2.0/ out/

WORKDIR /app/out
ENTRYPOINT [ "dotnet", "osu.Server.DifficultyCalculator.dll" ]