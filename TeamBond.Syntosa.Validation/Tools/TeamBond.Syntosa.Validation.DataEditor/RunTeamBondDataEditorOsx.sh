#!/usr/bin/env bash

# ##################################################
# Bash Script to run TeamBond Syntosa Data Editor
#
version="0.0.1"               # Sets version variable
#
# HISTORY:
#
# * DATE - v0.0.1  - 22-August-2020
#
# ##################################################

export TEAMBOND_APP_NAME="TeamBondDataEditor"
export AWS_PROFILE="default"
export AWS_REGION="us-east-2"
export TEAMBOND_APP_ENVIRONMENT="BLUE"

dotnet TeamBond.Syntosa.Validation.DataEditor.dll