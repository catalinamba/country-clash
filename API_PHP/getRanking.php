<?php

include "db.php";

$sql = "SELECT nombre, max_puntuacion 
        FROM Jugador 
        ORDER BY max_puntuacion DESC";

$result = $conn->query($sql);

$data = [];

while($row = $result->fetch_assoc()) {
    $data[] = $row;
}

echo json_encode(["players" => $data]);

?>