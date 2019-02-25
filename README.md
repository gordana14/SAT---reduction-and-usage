# SAT---reduction-and-usage
I implement two functionalities: 
1. Reduction of n-queens to SAT &amp; 
2. Reduction of graph coloring to SAT

*****1******
The n-queens is a well-known recreational mathematics problem. You are given an n by n chessboard,
out task is to put n queens on the chessboard, such that no queen is attacked by any other queen. 
This function accepts the dimensions of the chessboard, and prints the corresponding SAT problem in DIMACS format.

p cnf 3 3
1 2 3 0

The p on the first line indicates that it is the initialization of this SAT, cnf indicates that is it the conjunctive normal form, 
the first number is the number of variables, the second number is the number of clauses (also the number of lines that follow)
in this expression.
Each line that follows is one clause, which specified by giving the number of the variable. 
if the number is negative, the variable is negated.
Every clause is ended with the number 0.

********2******
Graph coloring is a problem of assigning colors to vertices of a graph, 
where no two vertices that share an edge are colored with the same color. 
The goal is to find a coloring with the least number of colors possible. 

Input a graph is in DIMACS format.


