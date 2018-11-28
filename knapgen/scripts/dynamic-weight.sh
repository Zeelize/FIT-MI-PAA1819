#!/bin/bash

for value in {7000..18000..1000}
do
	# run generator 
	echo $value
	../generator -n  25 -N 25 -m 0.5 -W $value -C 5000 -k 1 -d 0 > ../data/dynamic-weight/inst_$value.txt 
done

