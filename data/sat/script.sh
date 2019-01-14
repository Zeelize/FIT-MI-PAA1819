#!/bin/bash

for i in 1 2 3 4 5 6 7 8 9 10
do
	for value in 25 27 30 35 37 40
	do
	# run generator 
	./g3 $value 40 100 > ./sat_inst/sat_$value/inst_"$value"_$i.txt 
	done 
done

