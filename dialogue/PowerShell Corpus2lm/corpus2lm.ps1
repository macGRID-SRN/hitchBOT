<#
.SYNOPSIS
Builds a Langauge Model
.DESCRIPTION
Takes a Corpus file and performs the nessessary operations to get a language model.
.PARAMETER FileName
The name of the Corpus file you wish to process.
.EXAMPLE
corpus2lm -FileName corpus.txt
#>

param(
    [Parameter(Mandatory=$true)][string]$FileName
)

$WithoutExtension = [io.path]::GetFileNameWithoutExtension($FileName)

CMD /C "binary\\text2wfreq.exe < $FileName | binary\\wfreq2vocab.exe > out\\$WithoutExtension.vocab"
CMD /C "binary\\text2idngram -vocab out\\$WithoutExtension.vocab -idngram out\\$WithoutExtension.idngram < $FileName"
CMD /C "binary\\idngram2lm -vocab_type 0 -idngram out\\$WithoutExtension.idngram -vocab out\\$WithoutExtension.vocab -arpa out\\$WithoutExtension.arpa"
CMD /C "binary\\sphinx_lm_convert -i out\\$WithoutExtension.arpa -o out\\$WithoutExtension.lm.dmp"
CMD /C "x86-nt\\pronounce.exe -d voxforge_de_sphinx.dic -i out\\$WithoutExtension.vocab -o out\\$WithoutExtension.dic -e out\\$WithoutExtension.log"