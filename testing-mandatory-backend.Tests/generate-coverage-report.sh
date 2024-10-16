#! /bin/bash

# Correctly assign the variable without spaces
TOOL="dotnet-reportgenerator-globaltool"

# Check if the tool is installed
if ! dotnet tool list -g | grep -q "$TOOL"; then
    echo "No $TOOL found. Installing the tool..."
    dotnet tool install -g "$TOOL"
else 
    echo "The tool $TOOL is already installed."
fi

# Run tests and generate coverage report
dotnet test --collect:"Xplat Code Coverage"
reportgenerator "-reports:./TestResults/*/coverage.cobertura.xml" "-targetdir:CoverageReport" -reporttypes:Html
