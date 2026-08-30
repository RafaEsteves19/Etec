<?php

session_start();

if (!isset($_SESSION["tarefas"])) {
    $_SESSION["tarefas"] = [];
}

function adicionarTarefa(&$tarefas, $tarefa)
{
    if (!empty(trim($tarefa))) {
        $tarefas[] = trim($tarefa);
    }
}

function listarTarefas($tarefas)
{
    foreach ($tarefas as $indice => $tarefa) {
        echo "<li>" . ($indice + 1) . " - " . htmlspecialchars($tarefa) . "</li>";
    }
}

function contarTarefas($tarefas)
{
    return count($tarefas);
}

function limparTarefas(&$tarefas)
{
    $tarefas = [];
}

if ($_SERVER["REQUEST_METHOD"] === "POST") {

    if (isset($_POST["adicionar"])) {
        adicionarTarefa($_SESSION["tarefas"], $_POST["tarefa"] ?? "");
    }

    if (isset($_POST["limpar"])) {
        limparTarefas($_SESSION["tarefas"]);
    }

    header("Location: index.php");
    exit;
}

?>

<!DOCTYPE html>
<html lang="pt-BR">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lista de Tarefas</title>
    <link rel="stylesheet" href="style.css">
</head>

<body>

    <h1>Lista de Tarefas</h1>

    <form method="POST">

        <input
            type="text"
            name="tarefa"
            placeholder="Digite uma tarefa"
        >

        <button type="submit" name="adicionar">
            Adicionar
        </button>

    </form>

    <h2>Tarefas cadastradas</h2>

    <ul>
        <?php listarTarefas($_SESSION["tarefas"]); ?>
    </ul>

    <p>
        Total de tarefas:
        <?= contarTarefas($_SESSION["tarefas"]); ?>
    </p>

    <?php if (!empty($_SESSION["tarefas"])): ?>

        <form method="POST">
            <button type="submit" name="limpar">
                Limpar tarefas
            </button>
        </form>

    <?php endif; ?>

</body>

</html>
