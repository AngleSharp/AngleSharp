#!/usr/bin/env bash
###############################################################
# This is the Cake bootstrapper script that is responsible for
# downloading Cake and all specified tools from NuGet.
###############################################################

# Define directories.
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
TOOLS_DIR=$SCRIPT_DIR/tools
NUGET_EXE=$TOOLS_DIR/nuget.exe
NUGET_OLD_EXE=$TOOLS_DIR/nuget_old.exe
CAKE_EXE=$TOOLS_DIR/Cake/Cake.exe

# Define default arguments.
SCRIPT="build.cake"
TARGET="Default"
CONFIGURATION="Release"
VERBOSITY="verbose"
DRYRUN=
SHOW_VERSION=false
SCRIPT_ARGUMENTS=()

# Parse arguments.
for i in "$@"; do
    case $1 in
        -s|--script) SCRIPT="$2"; shift ;;
        -t|--target) TARGET="$2"; shift ;;
        -c|--configuration) CONFIGURATION="$2"; shift ;;
        -v|--verbosity) VERBOSITY="$2"; shift ;;
        -d|--dryrun) DRYRUN="-dryrun" ;;
        --version) SHOW_VERSION=true ;;
        --) shift; SCRIPT_ARGUMENTS+=("$@"); break ;;
        *) SCRIPT_ARGUMENTS+=("$1") ;;
    esac
    shift
done

# Make sure the tools folder exist.
if [ ! -d $TOOLS_DIR ]; then
  mkdir $TOOLS_DIR
fi

# Make sure that packages.config exist.
if [ ! -f $TOOLS_DIR/packages.config ]; then
    echo "Downloading packages.config..."
    curl -Lsfo $TOOLS_DIR/packages.config http://cakebuild.net/bootstrapper/packages
    if [ $? -ne 0 ]; then
        echo "An error occured while downloading packages.config."
        exit 1
    fi
fi

# Download NuGet (v3.5.0) if it does not exist.
if [ ! -f $NUGET_OLD_EXE ]; then
    echo "Downloading NuGet..."
    curl -Lsfo $NUGET_OLD_EXE https://dist.nuget.org/win-x86-commandline/v3.5.0/nuget.exe
    if [ $? -ne 0 ]; then
        echo "An error occured while downloading nuget.exe."
        exit 1
    fi
fi

# Download NuGet (latest) if it does not exist.
if [ ! -f $NUGET_EXE ]; then
    echo "Downloading NuGet..."
    curl -Lsfo $NUGET_EXE https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
    if [ $? -ne 0 ]; then
        echo "An error occured while downloading nuget.exe."
        exit 1
    fi
fi

# Restore tools from NuGet.
pushd $TOOLS_DIR >/dev/null
mono $NUGET_EXE install -ExcludeVersion
if [ $? -ne 0 ]; then
    echo "Could not restore NuGet packages."
    exit 1
fi
popd >/dev/null

# Make sure that Cake has been installed.
if [ ! -f $CAKE_EXE ]; then
    echo "Could not find Cake.exe at '$CAKE_EXE'."
    exit 1
fi

# Start Cake
if $SHOW_VERSION; then
    exec mono $CAKE_EXE -version
else
    exec mono $CAKE_EXE $SCRIPT -verbosity=$VERBOSITY -configuration=$CONFIGURATION -target=$TARGET $DRYRUN "${SCRIPT_ARGUMENTS[@]}"
fi