FROM mono:5.4

COPY . /src

RUN cd /src && ls -ls && nuget restore lunar_horizon.sln && msbuild /p:Configuration=Release /p:Platform="x64" lunar_horizon.sln

RUN ls /src/lunar_horizon/bin/x64/Release

WORKDIR /src/lunar_horizon/bin/x64/Release

CMD ["bash", "-c", "./lunar_horizon.bsh"]










