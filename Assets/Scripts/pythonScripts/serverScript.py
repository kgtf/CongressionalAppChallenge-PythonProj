import socket

# Server configuration
HOST = '127.0.0.1'  # Localhost (use 0.0.0.0 to allow external access)
PORT = 12345        # Port number

# Create a socket (IPv4, TCP)
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to an address and port
server_socket.bind((HOST, PORT))

# Listen for incoming connections (max 1 client in queue)
server_socket.listen(1)
print(f"Server listening on {HOST}:{PORT}...")


while True:
    conn, addr = server_socket.accept()
    print(f"Connected by {addr}")
    data = conn.recv(1024).decode('utf-8')
    if data.lower() == "exit":
        break
    print(f"Received from Unity: {data}")
    conn.sendall("Message received".encode('utf-8'))
