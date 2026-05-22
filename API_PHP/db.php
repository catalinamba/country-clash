<?php

$host = "ballast.proxy.rlwy.net";
$port = "49075";
$dbname = "railway";
$user = "root";
$password = "VrcPlROHtcVizsrJqvbrFcpgmVACslAe";

$conn = new mysqli($host, $user, $password, $dbname, $port);

if ($conn->connect_error) {
    die("Error de conexión: " . $conn->connect_error);
}

?>