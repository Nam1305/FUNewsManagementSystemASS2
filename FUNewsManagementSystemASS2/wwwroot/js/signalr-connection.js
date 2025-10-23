let connection;

export async function getConnection(role) {
    if (connection && connection.state === signalR.HubConnectionState.Connected) {
        return connection;
    }

    connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
        .withAutomaticReconnect()
        .build();

    connection.onclose(() => console.log("❌ Disconnected"));
    await connection.start();
    console.log("🔗 Connection started");

    return connection;
}
