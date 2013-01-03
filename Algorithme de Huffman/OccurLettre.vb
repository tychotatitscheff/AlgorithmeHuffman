Public Class OccurLettre
    ' Definition de la structure permettant de gérer le type FreqLettre
    Private Lettre As String
    Private Occurence As Integer

    ' Ajoute une variable contenant les structures occurlettre enfants
    Private OccurLettreEnfants() As OccurLettre

    ' Consultation et Affectation des etats (paramètres internes) de la classe OccurLettre
    Public Function Get_Lettre() As String
        Return Lettre
    End Function
    Public Sub Set_Lettre(ByVal lettreAAffecter As String)
        If Len(lettreAAffecter) = 1 Then
            Lettre = lettreAAffecter
        Else
            ' L'extérieur cherche à donner à l'OccurLettre plus qu'une lettre. On prend pour décision de sélectionner uniquement le premier caractère que ce que l'extérieur envoie à la classe
            ' On aurait également pu interdire l'affectation ou envoyer un message d'erreur.
            Lettre = Mid(lettreAAffecter, 1, 1)
        End If
    End Sub
    Public Sub Set_Occur(ByVal NbOccur As Integer)
        Occurence = NbOccur
    End Sub
    Public Function Get_Occur() As Integer
        Return Occurence
    End Function
    ' Petite procédure pour ajouter un élément à la lettre
    Public Sub AjouterOccurence()
        Occurence = Occurence + 1
    End Sub
    Public Sub Set_Enfants(ByVal Enfants() As OccurLettre)
        If Not (Enfants Is Nothing) Then
            If Enfants.GetLength(0) = 2 Then
                ' Vérifications que l'extérieur ne transmet exactement que 2 enfants
                OccurLettreEnfants = Enfants
            End If
        End If
    End Sub
    Public Function Get_Enfant(ByVal lequel As Integer) As OccurLettre
        Return OccurLettreEnfants(lequel)
    End Function

    Public Function RetournerEquivalenceTreenodes() As TreeNode
        ' Fonction qu'un élément peut appeler pour générer son équivalence sous la forme de controle treeview
        Dim Nouveau_TreeNode As New TreeNode()
        If Lettre = "" Then
            Nouveau_TreeNode.Text = "Composition" & " - " & Str(Occurence)
        Else
            Nouveau_TreeNode.Text = Lettre & " - " & Str(Occurence)
        End If
        If Not (OccurLettreEnfants Is Nothing) Then
            ' Il existe des éléments sous l'élement en cours d'analyse.
            Nouveau_TreeNode.Nodes.Add(OccurLettreEnfants(0).RetournerEquivalenceTreenodes)
            Nouveau_TreeNode.Nodes.Add(OccurLettreEnfants(1).RetournerEquivalenceTreenodes)
        End If
        Return Nouveau_TreeNode
    End Function

End Class
