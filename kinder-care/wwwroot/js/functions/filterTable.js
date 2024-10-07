function filterTable() {
    const input = document.getElementById("searchInput"); //Buscar elemento con ese id
    const filter = input.value.toLowerCase(); //Filtrar
    const table = document.getElementById("profilesTable"); //Table a filtrar
    const tr = table.getElementsByTagName("tr"); //Filas de table

    for (let i = 1; i < tr.length; i++) { //Empezar desde 1 para no contar el header
        const tds = tr[i].getElementsByTagName("td"); //Traer elementos del td
        let rowContainsText = false; //Iniciamos como false

        
        for (let j = 0; j < tds.length; j++) { 
            const td = tds[j];
            if (td) {
                if (td.textContent.toLowerCase().indexOf(filter) > -1) {
                    rowContainsText = true; // busca coincidencias y si las encuentra vuelve el Contains en true
                    break; 
                }
            }
        }

        tr[i].style.display = rowContainsText ? "" : "none"; //"" muestra la fila y none va a ocultarla
    }
}
