#!/bin/bash

nuget restore
# Options Debug|Release
CONFIGURATION=Release
msbuild ResxTranslator/ResxTranslator.csproj /property:Configuration=$CONFIGURATION
rm -rf $CONFIGURATION

mkdir -p $CONFIGURATION/lib
mkdir -p $CONFIGURATION/bin
mv ResxTranslator/bin/$CONFIGURATION $CONFIGURATION/lib/ResxTranslator
echo "#!/bin/sh
SCRIPT_DIR=\$( cd -- \"\$( dirname -- \"\${BASH_SOURCE[0]}\" )\" &> /dev/null && pwd )
exec mono \$SCRIPT_DIR/../lib/ResxTranslator/ResxTranslator.exe \"\$@\"" >> $CONFIGURATION/bin/resx_translator
chmod +x $CONFIGURATION/bin/resx_translator
