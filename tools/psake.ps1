# Helper script for those who want to run
# psake without importing the module.
import-module .\psake.psm1
invoke-psake -framework '4.0' @args
remove-module psake