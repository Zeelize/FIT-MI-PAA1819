#!/bin/bash

for value in {7000..15000..1000}
do
	# run generator 
	echo $value
	../generator -n  25 -N 25 -m 0.2 -W $value -C 5000 -k 1 -d 0 > ../data/bb-maxWeight/inst_$value.txt 
done

