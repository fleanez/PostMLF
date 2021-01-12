# PostMLF
OpenPLEXOS project for customizing the marginal loss factor and marginal costs components

# Description

This is an OpenPLEXOS addon that computes the ex-post marginal loss factor, also called Penalty Factor in some nomenclarute, for every busbar (or nodes) in a nodal OPF simulations. at the optimal solution. 
This calculation is performed even when no-losses have been considered during the optimization process. The results are stored in the custom pass-through "x" and "z" properties of the Node objects

# Marginal Loss Factor Calculation

Nodal Marginal Loss Factors are computed at the optimal solution as:

pf_i = 1 - dPerd/dP_i = 1 - sum(dPerd_k/dP_i) = 1 - sum(dPerd_k/dF_k * dF_k/dP_i) = 1 - sum(dPerd_k/dF_k * PTDF_k,i)

donde:

dPerd_k/dF_k: Marginal losses in branch "k"

dF_k/dP_i: It is the PTDF for branch flow "k" to an injection at node "i" 

# Usage Instructions:

The following steps are required in order to make this work in PLEXOS:
1. Compile: The created dll can be stored for example at the root of the PLEXOS xml database
2. Register DDL: Register de dll within the database (see PLEXOS help how to do it. Very simple!)
3. Configure Pass-Through: Enable Node's Property "z" in the PLEXOS Config. Do not introduce any values for this property because the dll will overwrite these vales before writing the solution.
4. Report Pass-Through: Don't forget to report Node's "z".

# Aknowledge

This library has been developed completely independent from PLEXOS but in collaboration and thanks to the participation of [Energy Exemplar](http://energyexemplar.com). Please visit their websites for awesome software products.
