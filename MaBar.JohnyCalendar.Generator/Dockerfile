# Base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Runtime image
FROM mcr.microsoft.com/dotnet/runtime:10.0-preview AS runtime
WORKDIR /app

# Install dependencies
RUN apt-get update && \
    apt-get install -y \
    tesseract-ocr \
    libtesseract-dev \
    libleptonica-dev \
    libc6 && \
    apt-get clean

# Copy tessdata directory
COPY tessdata /app/tessdata

# Set Tesseract data path
ENV TESSDATA_PREFIX=/app/tessdata

# Detect architecture and create symbolic links
RUN ARCH=$(dpkg --print-architecture) && \
    LIBDIR="" && \
    if [ "$ARCH" = "amd64" ]; then \
        LIBDIR=/usr/lib/x86_64-linux-gnu; \
    elif [ "$ARCH" = "arm64" ]; then \
        LIBDIR=/usr/lib/aarch64-linux-gnu; \
    else \
        echo "Unsupported architecture: $ARCH" && exit 1; \
    fi && \
    mkdir -p /app/x64 && \
    ln -sf ${LIBDIR}/liblept.so /app/x64/libleptonica-1.82.0.so && \
    ln -sf ${LIBDIR}/libtesseract.so.5 /app/x64/libtesseract50.so && \
    mkdir -p /usr/share/dotnet/shared/Microsoft.NETCore.App/10.0.0/ && \
    ln -sf ${LIBDIR}/libdl.so.2 /usr/share/dotnet/shared/Microsoft.NETCore.App/10.0.0/libdl.so && \
    ln -sf ${LIBDIR}/libdl.so.2 /app/libdl.so

# Copy application files
COPY --from=build /app/out /app

# Set the command to run the application
ENTRYPOINT ["dotnet", "MaBar.JohnyCalendar.Generator.dll"]
