<?php

include "db.php";

$nombre = $_POST['nombre'];
$puntuacion = $_POST['puntuacion'];
$pais = $_POST['pais'];

// comprobar si jugador existe
$check = $conn->query("SELECT * FROM Jugador WHERE nombre='$nombre'");

if ($check->num_rows > 0) {
    // actualizar max puntuación
    $conn->query("
        UPDATE Jugador 
        SET max_puntuacion = GREATEST(max_puntuacion, $puntuacion)
        WHERE nombre='$nombre'
    ");
} else {
    // crear jugador
    $conn->query("
        INSERT INTO Jugador(nombre, pais, max_puntuacion)
        VALUES('$nombre', '$pais', $puntuacion)
    ");
}

echo "OK";

?>