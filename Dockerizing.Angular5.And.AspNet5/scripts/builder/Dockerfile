FROM microsoft/aspnetcore-build:latest
ARG SCRIPT_URL=https://github.com/filipw/dotnet-script/releases/download/0.14.0/dotnet-script.0.14.0.zip

ENV PATH /usr/local/.yarn/bin:/usr/local/dotnet-script:/root/.yarn/bin:$PATH

# Add yarn package manager as apt-get source
RUN apt-get update && apt-get install apt-transport-https \
 && curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add - \
 && echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list

# Install unzip and yarn.
# Use curl and unzip to install dotnet-script. Use yarn to install angular/cli.
RUN apt-get update && apt-get install -y unzip yarn --no-install-recommends \
 && curl -SL $SCRIPT_URL --output /tmp/scriptcs.zip \
 && unzip -o /tmp/scriptcs.zip -d /usr/local/ \
 && chmod +x /usr/local/dotnet-script/dotnet-script.sh \
 && yarn global add @angular/cli --prefix /usr/local/.yarn

# Copy csx scripts
COPY csx /scripts/csx
COPY build.csx /scripts/build.csx

# Setup entry
WORKDIR /app
ENTRYPOINT ["dotnet-script.sh", "/scripts/build.csx", "--"]