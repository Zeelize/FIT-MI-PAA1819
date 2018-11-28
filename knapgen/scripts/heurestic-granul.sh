#!/bin/bash

for value in {0..6..1}
do
	# run generator 
	echo $value
	../generator -n  20 -N 20 -m 0.5 -W 30 -C 100 -k $value -d 1 > ../data/heurestic-granul/inst_$value.txt 
done

