#!/bin/bash

for value in {10000..40000..1000}
do
	# run generator 
	echo $value
	../generator -n  25 -N 25 -m 0.5 -W 30 -C $value -k 1 -d 1 > ../data/heurestic-price/inst_$value.txt 
done

