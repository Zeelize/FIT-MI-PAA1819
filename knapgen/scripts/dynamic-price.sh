#!/bin/bash

for value in {10000..20000..1000}
do
	# run generator 
	echo $value
	../generator -n  25 -N 25 -m 0.5 -W 200 -C $value -k 1 -d 0 > ../data/dynamic-price/inst_$value.txt 
done

