#!/usr/bin/expect -f

#Use this script to remotely connect to hitchBOT via VPN, reset all electronics with the exception of those deemed vital

# grab the password - adapted from http://stackoverflow.com/a/683386
stty -echo
send_user -- "Password for admin@10.8.0.4: "
expect_user -re "(.*)\n"
send_user "\n"
stty echo
set pass $expect_out(1,string)

spawn ssh admin@10.8.0.4
expect "admin@10.8.0.4's password: "

#check for user prompt, 'DeviceName>' but the device name can change so only check for '>'
send -- "$pass\r"
expect -re ".*>" 

#ensure connectivity
send "AT\r" 
expect "OK"
 
#disable power
send "AT+MIOOC=1,1\r"
expect "OK"

#make sure everything has enough time to power down
sleep 2
 
#reenable power
send "AT+MIOOC=1,0\r"
expect "OK"

#exit - restart completed
close 
