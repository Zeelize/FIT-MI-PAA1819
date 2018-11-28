#!/bin/bash

for value in 0.1 0.2 0.3 0.4 0.5 0.6 0.7 0.8 0.9 1
do
	# run generator 
	echo $value
	../generator -n  25 -N 25 -m $value -W 12000 -C 5000 -k 1 -d 0 > ../data/bb-cap-weight/inst_$value.txt 
done

