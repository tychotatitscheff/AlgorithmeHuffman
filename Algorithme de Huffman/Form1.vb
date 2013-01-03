Imports System.IO

' Correction V3 (Modification de la portée des variables de la classe OccuLettre) datée du 25/11/2012

Public Class Form1
    Public Function GenererTableauFrequence(ByVal message As String) As OccurLettre()
        Dim TablOccur(0) As OccurLettre
        TablOccur(0) = New OccurLettre
        TablOccur(0).Set_Lettre(Mid(message, 1, 1))
        TablOccur(0).Set_Occur(1)
        Dim LettreAAnalyser As String
        Dim position As Integer

        For i As Integer = 2 To message.Length
            LettreAAnalyser = Mid(message, i, 1)
            position = Existe(LettreAAnalyser, TablOccur)
            If position <> -1 Then
                TablOccur(position).AjouterOccurence()
            Else
                ReDim Preserve TablOccur(TablOccur.GetLength(0))
                TablOccur(TablOccur.GetLength(0) - 1) = New OccurLettre
                TablOccur(TablOccur.GetLength(0) - 1).Set_Lettre(LettreAAnalyser)
                TablOccur(TablOccur.GetLength(0) - 1).AjouterOccurence()
            End If
        Next
        Return TablOccur
    End Function

    Public Function Existe(ByVal LettreAAnalyser As String, ByVal Tableau() As OccurLettre) As Integer
        For i As Integer = 0 To Tableau.GetLength(0) - 1
            If LettreAAnalyser = Tableau(i).Get_Lettre Then
                Return i
            End If
        Next
        Return -1
    End Function

    Function TrierTableau(ByVal ElementsATrier() As OccurLettre) As OccurLettre()
        ' On utilise le tri bulle par permutation successives
        ' L'algo nécessite deux boucles imbriquées
        For i As Integer = ElementsATrier.GetLength(0) - 2 To 0 Step -1
            For j As Integer = 0 To i
                If ElementsATrier(j).Get_Occur > ElementsATrier(j + 1).Get_Occur Then
                    ' Permutation !
                    Dim FreLettreTemp As OccurLettre = ElementsATrier(j)
                    ElementsATrier(j) = ElementsATrier(j + 1)
                    ElementsATrier(j + 1) = FreLettreTemp
                End If
            Next
        Next
        Return ElementsATrier
    End Function

    ' Fonction qui a pour objectif de générer l'arbre binaire
    Private Function GenererArbre(ByVal Elements() As OccurLettre) As OccurLettre
        While Elements.GetLength(0) > 1
            ' On effectue le tri
            Elements = TrierTableau(Elements)

            ' On récupère les deux premiers élements contenus dans le tableau et on le stock dans un tableau temporaire
            Dim Enfants(1) As OccurLettre
            Enfants(1) = Elements(1)
            Enfants(0) = Elements(0)

            ' On crée la nouvelle lettre contenant les deux elements enfants
            Dim NouvelleLettre As New OccurLettre
            NouvelleLettre.Set_Enfants(Enfants)
            ' On lui associe son occurence : la somme des occurences de ses deux élements enfants
            NouvelleLettre.Set_Occur(Enfants(0).Get_Occur + Enfants(1).Get_Occur)

            ' On met à jour le tableau contenant l'ensemble des noeuds (on en supprime deux éléments pour en ajouter un nouveau)
            For i As Integer = 2 To Elements.GetLength(0) - 1
                Elements(i - 1) = Elements(i)
            Next
            Elements(0) = NouvelleLettre
            ReDim Preserve Elements(Elements.GetLength(0) - 2)
        End While

        Return Elements(0)
    End Function

    Private Sub Validation_ArbreBinaire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Validation_ArbreBinaire.Click
        ' Procédure qui a pour objectif d evérifier que les premières fonctions + la génération de l'arbre fonctionnent parfaitement
        ' L'appel de la génération de l'arbre ne prend qu'une ligne
        Dim RacineArbreBinaire As OccurLettre = GenererArbre(GenererTableauFrequence(TextBox1.Text))
        MaJ_Treeview(RacineArbreBinaire)
    End Sub

    Private Sub MaJ_Treeview(ByVal ElementAAfficher As OccurLettre)
        ' Procèdure qui a pour objectif de mettre à jour un contrôle spécifique : le treeview. Parfait pour afficher un arbre... binaire !
        ' Effacement du contenu de l'arbre
        TreeView1.Nodes.Clear()
        ' Puisque ce sont les éléments qui savent générer leur représentation sous la forme de noeuds, il suffit de demander à l'élement de plus haut niveau de nous retourner sa représentation :
        TreeView1.Nodes.Add(ElementAAfficher.RetournerEquivalenceTreenodes)
    End Sub
End Class
