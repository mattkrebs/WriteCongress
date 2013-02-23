<?

	$success = false;
	if( isset($_REQUEST['cSubmit']) ) {
	
		$error = false;
		
		$cName = trim(htmlentities($_REQUEST['cName']));
		$cPhone = trim(htmlentities($_REQUEST['cPhone']));
		$cEmail = trim(htmlentities($_REQUEST['cEmail']));
		$cMessage = trim(htmlentities($_REQUEST['cMessage']));
		
		$mailBody = "<b>Name: </b>".$cName."<br>\n".
					"<b>Phone: </b>".$cPhone."<br>\n".
					"<b>Email: </b>".$cEmail."<br>\n".
					"<b>Message: </b>".$cMessage."<br>\n";
					
		if( empty($cName) ) {
			$error = true;
			$errorMsg .= "You Must Supply a First and Last Name. <br />";
		}
		
		if( empty($cEmail) ) {
			$error = true;
			$errorMsg .= "You Must Supply an Email Address <br />";
		}

		require_once( "class.phpmailer.php" );
		$mail = new PHPMailer();
		$mail->IsHTML(true);
		$mail->Subject = "Campaign HTML - Contact Form"; //--------------- You need to change the website name here
		$mail->AddAddress($cEmail, "NAME@WEBSITE.com"); //--------------- You need to change the email address here. $cEmail sends a copy to the sender. 
		$mail->From = $cEmail;
		$mail->FromName = $cName;
		$mail->Body = $mailBody;

		if( !$error ) {
			if( $mail->Send() ) {
				$success = true;
				$successMsg = "Message Sent Successfully. We will contact you shortly regarding your comments/questions.";
			} else {
				$error = true;
				$errorMsg = "Unable to Send Message. Please Try Again.";
			}
		}

	}


?>
<?php include('includes/header.php'); ?>

<div class="posts-wrap" id="page">
	<h2 class="post_title">Contact Us</h2>
	<? if( $error ) echo "<div class=\"warning\"><p>".$errorMsg."</p></div><div class=\"clear\"></div>";
	if( $success ) echo "<div class=\"alert\"><p>".$successMsg."</p></div><div class=\"clear\"></div>";				
	if( !$success ) { ?>
	<form method="post" action="page-contact.php" id="cForm" class="form"> <?php /* you must change the action page name if you change the filename of this page */ ?>
		<p>
			<label>Name*</label>
			<input name="cName" type="text" id="cName" size="35" value="<? echo $_POST['cName']; ?>" />
		</p>
		<p>
			<label>Phone</label>
			<input name="cPhone" type="text" id="cPhone" size="35" value="<? echo $_POST['cPhone']; ?>" />
		</p>
		<p>
			<label>Email*</label>
			<input name="cEmail" type="text" id="cEmail" size="35" value="<? echo $_POST['cEmail']; ?>" />
		</p>
		<p>
			<label>Message</label>
			<textarea name="cMessage" cols="30" rows="5" id="cMessage"><? echo $_POST['cMessage']; ?></textarea>
		</p>
		<p>
			<input type="submit" name="cSubmit" id="cSubmit" value="Submit" />
		</p>
		<p>*denotes required</p>
	</form>
	<? } ?>
	<div class="clear"></div>
</div><!-- End .posts-wrap-->

<?php include('includes/sidebar.php'); ?>
<?php include('includes/footer.php'); ?>